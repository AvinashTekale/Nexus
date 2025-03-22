using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Entities
{
    public class Equipment : BaseEntity
    {
        public string SerialNumber { get; set; }

        [Required]
        public int EquipmentModelId { get; set; }

        [ForeignKey("EquipmentModelId")]
        public virtual EquipmentModel EquipmentModel { get; set; }

        [Required]
        public int EquipmentCategoryId { get; set; }

        [ForeignKey("EquipmentCategoryId")]
        public virtual EquipmentCategory EquipmentCategory { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [Required]
        public int HospitalId { get; set; }

        [ForeignKey("HospitalId")]
        public virtual Hospital Hospital { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        [Required]
        public string EquipmentImportance { get; set; } // Critical / Non-Critical

        [Required]
        public DateTime PurchaseDate { get; set; }

        public DateTime WarrantyStart { get; set; }

        public DateTime WarrantyEnd { get; set; }

        [Required]
        public int MaintenanceScheduleFrequency { get; set; } // Frequency in years (1, 2, 3, etc.)

        public DateTime InstallationDate { get; set; }

        public bool CalibrationRequired { get; set; }

        // Calculated Next Maintenance Date (based on Maintenance Frequency and Installation Date)
        public DateTime NextMaintenanceDate { get; set; }

        // Calculated Next Calibration Date (1 year after the installation date, or based on your requirements)
        public DateTime NextCalibrationDate
        {
            get
            {
                return InstallationDate.AddYears(1); // You can adjust this logic based on your needs
            }
        }
    }
}
