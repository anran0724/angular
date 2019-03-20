using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.EccpBaseSaicUnits.Dtos
{
    public class GetEccpBaseSaicUnitForEditOutput
    {
		public EditEccpBaseSaicUnitDto EccpBaseSaicUnit { get; set; }

		public string ECCPBaseAreaName { get; set;}

		public string ECCPBaseAreaName2 { get; set;}

		public string ECCPBaseAreaName3 { get; set;}

		public string ECCPBaseAreaName4 { get; set;}


    }
}