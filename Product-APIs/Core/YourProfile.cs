using AutoMapper;
using Product_APIs.DTOs;
using Product_APIs.Model;

namespace Product_APIs.Core
{
    public class YourProfile:Profile
    {
        public YourProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap().ForMember(pro => pro.Id, opt => opt.Ignore()); ;
        }
    }
}
