using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using PerpusBelajar.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerpusBelajar.ViewModels
{
    public class BookEditViewModel : BookCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingImagePath { get; set; }
        public string PageTitle { get; set; }
    }
}
