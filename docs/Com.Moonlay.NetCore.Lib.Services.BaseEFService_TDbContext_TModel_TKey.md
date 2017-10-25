### [Com.Moonlay.NetCore.Lib.Services.BaseEFService<TDbContext, TModel, TKey>]
Abstract implementation from [`Com.Moonlay.NetCore.Lib.Services.IBaseEFService<TDbContext, TModel, TKey>`][Com.Moonlay.NetCore.Lib.docs.BaseEFService<TDbContext, TModel, TKey>].


##### Generic Parameters

|Name													| Description|
|---													| --- |
|TDbContext												| Instance of `Microsoft.EntityFrameworCore.DbContext`|
|TModel													| Model type, should be an implementation of `Com.Moonlay.Models.IEntity<TKey>`, `System.ComponentModel.DataAnnotations.IValidatableObject`|
|TKey													| Key type, should be an implementation of `System.IConvertible` |


##### Properties

|Name								| Description|
|---								| ---|
|IServiceProvider ServiceProvider	| Implementation of `System.IServiceProvider`|
|TDbContext DbContext				| EF DbContext |


##### Methods

|Returns				| Name									| Description |
|---					| ---									| --------    |
|*IEnumerable\<TModel>*	| Get()									| Return all object. |
|*Task\<TModel>*		| GetAsync(TKey id)						| Return object with specified `id` asynchronously. Include query filter.|
|*TModel*				| Get(TKey id)							| Return object with specified `id`. Include query filter.|
|*TModel*				| Find(params object[] keys)			| Return object with specified `keys`. Ignore query filter.|
|*Task\<TModel>*		| FindAsync(params object[] keys)		| Return object with specified `keys` asynchronously. Ignore query filter.|
|*int*					| Create(TModel model)					| Create/persist supplied `object`. Return an integer indicating number of affected record. |
|*Task\<int>*			| CreateAsync(TModel model)				| Create/persist supplied `object` asynchronously. Return Task which resolve an integer indicating number of affected record. |
|*int*					| Update(TKey id, TModel model)			| Update data with id `id` with supplied `model`. Return an integer indicating number of affected record. |
|*Task\<int>*			| UpdateAsync(TKey id, TModel model)	| Update data with id `id` with supplied `model` asynchronously. Return Task which resolve an integer indicating number of affected record. |
|*int*					| Delete(TKey id)						| Delete data by `id`.Do soft delete if data is `Com.Moonlay.Models.ISoftEntity`. Return an integer indicating number of affected record. |
|*Task\<int>*			| DeleteAsync(TKey id)					| Delete data by `id` asynchronously. Do soft delete if data is `Com.Moonlay.Models.ISoftEntity`. Return Task which resolve an integer indicating number of affected record. |
|*int*					| Destroy(TKey id)						| Delete data physically by `id`. Return an integer indicating number of affected record. |
|*Task\<int>*			| DestroyAsync(TKey id)					| Delete data physically by `id` asynchronously. Return Task which resolve an integer indicating number of affected record. |
|*bool*					| IsExists(TKey id)						| Check if data with specified `id` exists. Return boolean. |





[Com.Moonlay.NetCore.Lib.docs.BaseEFService<TDbContext, TModel, TKey>]: ./Com.Moonlay.NetCore.Lib.Services.BaseEFService_TDbContext_TModel_TKey.md
[Com.Moonlay.NetCore.Lib.Services.BaseEFService<TDbContext, TModel, TKey>]: ../Com.Moonlay.NetCore.Lib/Services/IBaseEFService.cs