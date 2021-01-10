using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceService.Models
{
    [Table("device", Schema = "phoenix")]
    public class Device
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("serial")]
        public string Serial { get; set; }
        [Column("type")]
        public string Type { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}