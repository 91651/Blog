﻿using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities;

public class File
{
    [Key]
    [MaxLength(40)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [MaxLength(40)]
    public string OwnerId { get; set; }

    [MaxLength(200)]
    public string Name { get; set; }

    [MaxLength(300)]
    public string Path { get; set; }

    [MaxLength(40)]
    public string Md5 { get; set; }
}