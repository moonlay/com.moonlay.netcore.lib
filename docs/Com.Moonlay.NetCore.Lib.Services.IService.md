### [Com.Moonlay.NetCore.Lib.Services.IService]
Interface for all service implementation. 


#### methods

|Returns				| Name									| Description |
|---					| ---									| --------    |
|*IEnumerable\<object>*	| Get()									| Return all object. |
|*Task\<object>*		| GetAsync(object id)					| Return object with specified `id` asynchronously. Include query filter.|
|*object*				| Get(object id)						| Return object with specified `id`. Include query filter.|
|*object*				| Find(params object[] keys)			| Return object with specified `keys`. Ignore query filter.|
|*Task\<object>*		| FindAsync(params object[] keys)		| Return object with specified `keys` asynchronously. Ignore query filter.|
|*int*					| Create(object model)					| Create/persist supplied `object`. Return an integer indicating number of affected record. |
|*Task\<int>*			| CreateAsync(object model)				| Create/persist supplied `object` asynchronously. Return Task which resolve an integer indicating number of affected record. |
|*int*					| Update(object id, object model)		| Update data with id `id` with supplied `model`. Return an integer indicating number of affected record. |
|*Task\<int>*			| UpdateAsync(object id, object model)	| Update data with id `id` with supplied `model` asynchronously. Return Task which resolve an integer indicating number of affected record. |
|*int*					| Delete(object id)						| Delete data by `id`.Do soft delete if data is `Com.Moonlay.Models.ISoftEntity`. Return an integer indicating number of affected record. |
|*Task\<int>*			| DeleteAsync(object id)				| Delete data by `id` asynchronously. Do soft delete if data is `Com.Moonlay.Models.ISoftEntity`. Return Task which resolve an integer indicating number of affected record. |
|*int*					| Destroy(object id)					| Delete data physically by `id`. Return an integer indicating number of affected record. |
|*Task\<int>*			| DestroyAsync(object id)				| Delete data physically by `id` asynchronously. Return Task which resolve an integer indicating number of affected record. |
|*bool*					| IsExists(object id)					| Check if data with specified `id` exists. Return boolean. |





[Com.Moonlay.NetCore.Lib.docs.IService]: ./Com.Moonlay.NetCore.Lib.Services.IService.md
[Com.Moonlay.NetCore.Lib.Services.IService]: ../Com.Moonlay.NetCore.Lib/Services/IService.cs