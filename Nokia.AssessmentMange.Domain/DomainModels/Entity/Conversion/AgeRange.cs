namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 年龄范围
    /// </summary>
    public class AgeRange : Range<int>
    {

        protected AgeRange() { }
        public AgeRange(int floorAge, int cellingAge)
        {
            if (floorAge > cellingAge) { throw new Exceptions.AgeRangeError(cellingAge, floorAge); }
            this.CellingAge = cellingAge;
            this.FloorAge = floorAge;
        }

        public int CellingAge { get { return Maximum; } set { Maximum = value; } }
        public int FloorAge { get { return Minimum; } set { Minimum = value; } }

    }
}
