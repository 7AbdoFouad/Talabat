using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductsWithFilterationForCountSpecifications : BaseSpecifications<Product>
    {
        public ProductsWithFilterationForCountSpecifications(ProductSpecParams SpecParams)
            : base(P =>
                      (string.IsNullOrEmpty(SpecParams.Search) || P.Name.ToLower().Contains(SpecParams.Search)) &&
                      (!SpecParams.BrandId.HasValue || P.BrandId == SpecParams.BrandId.Value) &&
                      (!SpecParams.CategoryId.HasValue || P.CategoryId == SpecParams.CategoryId.Value)
                  )
        {

        }
    }
}
