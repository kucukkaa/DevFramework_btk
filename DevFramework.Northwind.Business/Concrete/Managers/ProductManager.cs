using AutoMapper;
using DevFramework.Core.Aspects.Postsharp;
using DevFramework.Core.Aspects.Postsharp.AuthhorizationAspects;
using DevFramework.Core.Aspects.Postsharp.CacheAspect;
using DevFramework.Core.Aspects.Postsharp.LogAspects;
using DevFramework.Core.Aspects.Postsharp.TransactionAspects;
using DevFramework.Core.Aspects.Postsharp.ValidationAspects;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using DevFramework.Core.Utilities.Mappings;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Add(Product product)
        {
            return _productDal.Add(product);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SucuredOperation(Roles="Admin")]
        public List<Product> GetAll()
        {
            //return _productDal.GetList().Select(p => new Product { 
            //    ProductId = p.ProductId,
            //    CategoryId = p.CategoryId,
            //    QuantityPerUnit = p.QuantityPerUnit,
            //    ProductName = p.ProductName,
            //    UnitPrice = p.UnitPrice
            //}).ToList();

            return _mapper.Map<List<Product>>(_productDal.GetList());
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public Product GetById(int ProductId)
        {
            return _productDal.Get(p => p.ProductId == ProductId);
        }

        //[TransactionScopeAspect]
        //public void TransactionalOperation(Product product1, Product product2)
        //{
        //    _productDal.Add(product1);
        //    //Business Codes
        //    _productDal.Update(product2);
        //}

        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }

        
    }
}
