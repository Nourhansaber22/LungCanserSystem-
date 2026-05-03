using LungCancer.Domain.Enums;
using Luvia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancer.Domain.Entities
{
    public class Scan
    {
        public int Id { get; private set; }

        public int PatientId { get; private set; }
        public int UploadedById { get; private set; }

        public string FilePath { get; private set; }
        public string FileName { get; private set; }
        public int? FileSizeKb { get; private set; }

        public DateTime ScanDate { get; private set; }
        public DateTime UploadDate { get; private set; }

        public ScanStatus Status { get; private set; }

        public string? Notes { get; private set; }

        public Patient Patient { get; private set; }
        public User UploadedBy { get; private set; }

        public SmartReport? SmartReport { get; private set; }

        private Scan() { }

        public Scan(int patientId,
                    int uploadedById,
                    string filePath,
                    string fileName,
                    int? fileSizeKb,
                    DateTime scanDate,
                    string? notes)
        {
            PatientId = patientId;
            UploadedById = uploadedById;
            FilePath = filePath;
            FileName = fileName;
            FileSizeKb = fileSizeKb;
            ScanDate = scanDate;
            Notes = notes;

            Status = ScanStatus.Pending;
            UploadDate = DateTime.UtcNow;
        }

        public void MarkAsProcessing() => Status = ScanStatus.Processing;
        public void MarkAsCompleted() => Status = ScanStatus.Completed;
        public void MarkAsFailed() => Status = ScanStatus.Failed;
    }
}
