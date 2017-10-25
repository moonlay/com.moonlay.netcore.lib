# com.moonlay.netcore.lib
This library is used for moonlay standard implementation for:
  - CRUD Service
   

## Basic Usage

#### Model

```csharp
using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
public class TestModel : StandardEntity, IValidatableObject
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(this.Code))
            yield return new ValidationResult("Code is required", new List<string> { "Code" });

        if (string.IsNullOrWhiteSpace(this.Name))
            yield return new ValidationResult("Name is required", new List<string> { "Name" });

        if (!string.IsNullOrWhiteSpace(this.Description) && this.Description.Length > 255)
            yield return new ValidationResult("Exceeded length", new List<string> { "Description" });
    }
} 

```


#### DbContext

```csharp
using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
 
public class TestDbContext : BaseDbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    } 
    public DbSet<TestModel> TestModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
} 
```


#### Service

```csharp
using Com.Moonlay.NetCore.Lib.Service;
using System;

public class TestModelService : StandardEntityService<TestDbContext, TestModel>
{
    public TestModelService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
```


## dependencies

  - com.moonlay.models
  - com.moonlay.entityframeworkcore
   

## interfaces 

  - [Com.Moonlay.NetCore.Lib.Services.IService]
  - [Com.Moonlay.NetCore.Lib.Services.IService<TModel, TKey>]
  - [Com.Moonlay.NetCore.Lib.Services.IBaseEFService]
  - [Com.Moonlay.NetCore.Lib.Services.IBaseEFService<TDbContext, TModel, TKey>]


## classes 

  - [Com.Moonlay.NetCore.Lib.Services.BaseEFService<TDbContext, TModel, TKey>]
  - [Com.Moonlay.NetCore.Lib.Services.StandardEntityService<TDbContext, TModel>]



[Com.Moonlay.NetCore.Lib.Services.IService]: ./docs/Com.Moonlay.NetCore.Lib.Services.IService.md
[Com.Moonlay.NetCore.Lib.Services.IService<TModel, TKey>]: ./docs/ComCom.Moonlay.NetCore.Lib.Services.IService_TModel_TKey.md
[Com.Moonlay.NetCore.Lib.Services.IBaseEFService]: ./docs/Com.Moonlay.NetCore.Lib.Services.IBaseEFService.md
[Com.Moonlay.NetCore.Lib.Services.IBaseEFService<TDbContext, TModel, TKey>]: ./docs/Com.Moonlay.NetCore.Lib.Services.IBaseEFService_TDbContext_TModel_TKey.md
[Com.Moonlay.NetCore.Lib.Services.BaseEFService<TDbContext, TModel, TKey>]: ./docs/Com.Moonlay.NetCore.Lib.Services.BaseEFService_TDbContext_TModel_TKey.md 
[Com.Moonlay.NetCore.Lib.Services.StandardEntityService<TDbContext, TModel>]: ./docs/Com.Moonlay.NetCore.Lib.Services.StandardEntityService_TDbContext_TModel.md 