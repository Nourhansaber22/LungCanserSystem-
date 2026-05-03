using LungCancer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancer.Domain.Entities
{
     public class Nodule
    {
        public int Id { get; private set; }

        public int ReportId { get; private set; }
        public int NoduleIndex { get; private set; }

        public decimal? VolumeCm3 { get; private set; }
        public decimal? DiameterMm { get; private set; }

        public decimal MalignancyScore { get; private set; }

        public NoduleClassification Classification { get; private set; }

        public string? Location { get; private set; }
        public string? Morphology { get; private set; }

        public int? CoordinatesX { get; private set; }
        public int? CoordinatesY { get; private set; }
        public int? CoordinatesZ { get; private set; }

        public SmartReport Report { get; private set; }

        private Nodule() { }
    }
}
