using System;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        private Speed Speed { get; set; }

        public UnitTest1()
        {
            Speed = new Speed(10, new KmPerHour());
        }

        [Fact]
        public void Test001_ASpeedValueWhenIncreasePercentShouldUpdateValue()
        {
            //setup Testee
            //execute (estimulo)
            Speed.IncreasePercent(20);
            //assertions (verificaciones)
            Assert.Equal(12, Speed.Value);
            //tear down environment
        }

        [Fact]
        public void Test002_ASpeedValueWhenIncreaseAbsolutShouldUpdateValue()
        {
            Speed.IncreaseAbsolut(20);
            Assert.Equal(30, Speed.Value);
        }

        [Fact]
        public void Test003_SpeedValueWhenDecreasedBelowZeroShouldFailAndNotUpdate()
        {
            Assert.Throws<InvalidOperationException>(() => Speed.IncreaseAbsolut(-20));
            Assert.Equal(10, Speed.Value);
        }

        [Fact]
        public void Test004_()
        {
            Speed = new Speed(36, new KmPerHour());
            Speed.ChangeUnit(new MeterPerSecond());
            Assert.Equal(10, Speed.Value, 10);
            Assert.True(Speed.Unit is MeterPerSecond);
        }

        [Fact]
        public void Test005()
        {
            KmPerHour kmPerHour = new KmPerHour();
            MeterPerSecond meterPerSecond = new MeterPerSecond();
            var ratio = kmPerHour.GetRatio(meterPerSecond);
            Assert.Equal(3.6, ratio, 10);
        }
    }

    public class Speed
    {
        public double Value { get; private set; }
        public Unit Unit { get; private set; }

        public Speed(double value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        public void IncreasePercent(double percentage)
        {
            Value *= percentage / 100 + 1;
        }

        public void IncreaseAbsolut(double valueToIncrease)
        {
            if (Value + valueToIncrease < 0)
                throw new InvalidOperationException();
            Value += valueToIncrease;
        }

        internal void ChangeUnit(Unit newUnit)
        {
            Value /= Unit.GetRatio(newUnit);
            Unit = newUnit;
        }
    }

    public abstract class Unit
    {
        public double GetRatio(Unit unit)
        {
            return GetRatioToMaster() / unit.GetRatioToMaster();
        }

        public abstract double GetRatioToMaster();
    }

    public class KmPerHour : Unit
    {
        public override double GetRatioToMaster()
        {
            return 1;
        }
    }

    public class MeterPerSecond : Unit
    {
        public override double GetRatioToMaster()
        {
            return 1 / 3.6;
        }
    }
}
