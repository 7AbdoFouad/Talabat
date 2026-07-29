using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        // This Constructor will be Used fol Creating an Object, That will be Used to Get All Products
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams SpecParams)
            : base(P =>
                      (string.IsNullOrEmpty(SpecParams.Search) || P.Name.ToLower().Contains(SpecParams.Search)) &&
                      (!SpecParams.BrandId.HasValue || P.BrandId == SpecParams.BrandId.Value) &&
                      (!SpecParams.CategoryId.HasValue || P.CategoryId == SpecParams.CategoryId.Value)
                  )
        {
            AddIncludes();
            if (!string.IsNullOrEmpty(SpecParams.Sort))
            {
                switch (SpecParams.Sort)
                {
                    case "priceAsc":
                        //OrderBy = P => P.Price;
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        //OrderByDesc = P. => P.Price;
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
                AddOrderBy(P => P.Name);

            // totalProducts = 18 ~ 20
            // pageSize =5
            // pageIndex= 3
            ApplyPagination((SpecParams.PageIndex - 1) * SpecParams.PageSize, SpecParams.PageSize);
        }
        //This Constructor will be Used fol Creating an Object, That will be Used to Get a Specific Product with Id
        public ProductWithBrandAndCategorySpecifications(int id) : base(P => P.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}