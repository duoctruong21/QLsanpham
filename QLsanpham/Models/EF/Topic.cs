﻿using System;
using System.Collections.Generic;

namespace QLsanpham.Models.EF;

public partial class Topic
{
    public int Id { get; set; }

    public string? TopicName { get; set; }

    public string? TopicDescription { get; set; }

    public string? TopicImg { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Alias { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();

    public virtual ICollection<Topicwithsong> Topicwithsongs { get; set; } = new List<Topicwithsong>();
}
