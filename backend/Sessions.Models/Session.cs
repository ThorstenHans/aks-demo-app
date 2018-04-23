using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sessions.Models
{
    public class Session
    {
        public Session()
        {
            Level = 100;
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Abstract { get; set; }
        public DateTime Date { get;set; }

        [Required]
        public string Conference { get; set; }

        [Required]
        [MaxLength(255)]
        public string Speaker { get; set; }
        
        [Required]
        public int Level { get; set; } 
        public Audience Audience { get; set; }

        public void UpdateValuesFrom(Session other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            Title = other.Title;
            Abstract = other.Abstract;
            Date = other.Date;
            Conference = other.Conference;
            Speaker = other.Speaker;
            Level = other.Level;
            Audience = other.Audience;
        }

    }
}
