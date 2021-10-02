using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using DevFramework.Northwind.Entities.Concrete;

/// <summary>
/// Summary description for ProductService
/// </summary>
public class ProductService : IProductService
{
    public ProductService()
    {

    }
    
    private IProductService _productService = InstanceFactory.GetInstance<IProductService>();
    public Product Add(Product product)
    {
        return _productService.Add(product);
    }

    public List<Product> GetAll()
    {
        return _productService.GetAll();
    }

    public Product GetById(int ProductId)
    {
        return _productService.GetById(ProductId);
    }

    public Product Update(Product product)
    {
        return _productService.Update(product);
    }
}