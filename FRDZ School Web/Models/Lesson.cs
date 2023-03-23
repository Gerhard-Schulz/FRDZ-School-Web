﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FRDZ_School_Web.Models
{
    public class Lesson
    {
        [Key]
        [Column("Lesson_Num")]
        [DisplayName("Номер")]
        public int Id { get; set; }

        [DisplayName("Описание")]
        public string? Lesson_Description { get; set; }

        [DisplayName("Предмет")]
        [Required]
        public int Code_Obj { get; set; }

        [DisplayName("Дата проведения")]
        [Required]
        public DateTime Date_And_Time { get; set; }

        [DisplayName("Учитель")]
        [Required]
        public int Teacher_Num { get; set; }

        [DisplayName("Домашнее задание")]
        public string? Home_Task { get; set; }
    }
}
