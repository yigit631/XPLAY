
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DAL
{
    [Table("Games")]
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Benzersiz Oyun Kimliği

        [Required]
        [StringLength(100, ErrorMessage = "Game name cannot exceed 100 characters.")]
        public string Name { get; set; } // Oyun Adı

        
        public DateTime? ReleaseDate { get; set; } // Çıkış Tarihi

        [Required]
        public decimal Price { get; set; } // Fiyat

        // Publisher ile ilişkisi
        public int PublisherId { get; set; } // Yayınevi Kimliği (Foreign Key)
        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; } // Yayınevi Referansı
    }
}
