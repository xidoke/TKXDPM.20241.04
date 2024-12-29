using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIMS.Services;
using AIMS.Services.impl;

namespace AIMS.Controllers.Address
{
    public class AddressController
    {
        private readonly IProvinceService provinceService;
        private readonly IDistrictService districtService;
        private readonly IWardService wardService;

        public AddressController(IProvinceService provinceService, IDistrictService districtService)
        {
            this.provinceService = provinceService;
            this.districtService = districtService;
        }

        public async Task GetProvincesAsync(ComboBox cbxProvince)
        {
            var provinces = await provinceService.GetAllProvincesAsync();
            foreach (var province in provinces)
            {
                cbxProvince.Items.Add(province.Name);
            }
        }

        public async Task GetDistrictsByProvinceAsync(ComboBox cbxCity, ComboBox cbxDistrict)
        {
            if (!string.IsNullOrEmpty(cbxCity.SelectedItem?.ToString()))
            {
                var selectedProvince = await provinceService.GetProvinceByNameAsync(cbxCity.SelectedItem.ToString());
                var districts = await districtService.GetDistrictsByProvinceAsync(selectedProvince?.Id);
                foreach (var district in districts)
                {
                    cbxDistrict.Items.Add(district.Name);
                }
            }
        }

        public async Task GetWardsByDistrictAsync(ComboBox cbxCity, ComboBox cbxDistrict, ComboBox cbxWard)
        {
            if (!string.IsNullOrEmpty(cbxCity.SelectedItem?.ToString()) && !string.IsNullOrEmpty(cbxDistrict.SelectedItem?.ToString()))
            {
                var selectedDistrict = await districtService.GetDistrictByNameAsync(cbxDistrict.SelectedItem.ToString());
                var wards = await wardService.GetWardsByDistrictAsync(selectedDistrict?.Id);
                foreach (var ward in wards)
                {
                    cbxWard.Items.Add(ward.Name);
                }
            }
        }
    }
}
