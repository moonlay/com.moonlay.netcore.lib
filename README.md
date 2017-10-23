# com.moonlay.netcore.lib
This library is used for moonlay standard implementation for:
  - Service

## namespaces
  - com.moonlay.netcore.lib.services

## dependencies
  - com.moonlay.models
  - com.moonlay.entityframeworkcore

### com.moonlay.netcore.lib.services
#### interfaces 

##### IService
Interface for all service implementation. 
| Method												| Description |
| ------------------------------------					| --------    |
| *IEnumerable<object>* Get()							| return all object. |
| *object* Get(object id)								| return object with specified `id` object. |
| *int* Create(object model)							| create/persist supplied `model` object. Return an integer indicating number of affected record. |
| *Task<int>* CreateAsync(object model)					| create/persist supplied `model` object asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *int* Update(object id, object model)					| update data with id `id` with supplied `model` object. Return an integer indicating number of affected record. |
| *Task<int>* UpdateAsync(object id, object model)		| update data with id `id` with supplied `model` object asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *int* Delete(object id)								| delete data with id `id`. Return an integer indicating number of affected record. |
| *Task<int>* DeleteAsync(object id)					| delete data with id `id` asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *bool* IsExists(object id)							| check if data with specified `id` exists. Return boolean. |

##### IService<TModel, TKey>
Generic interface for all service implementation, inherited from `Com.Moonlay.NetCore.Lib.Services.IService`.

|Generic Parameter										| Description|
|-----------------										| -----------|
|TModel													| Model type, should be an implementation of `Com.Moonlay.Models.IEntity`|
|TKey													| Key type, should be an implementation of `System.IConvertible` |

| Method												| Description |
| ------------------------------------					| --------    |
| *IEnumerable<TModel>* Get()							| return all object. |
| *object* Get(TKey id)									| return object with specified `id` object. |
| *int* Create(TModel model)							| create/persist supplied `model` object. Return an integer indicating number of affected record. |
| *Task<int>* CreateAsync(TModel model)					| create/persist supplied `model` object asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *int* Update(TKey id, TModel model)					| update data with id `id` with supplied `model` object. Return an integer indicating number of affected record. |
| *Task<int>* UpdateAsync(TKey id, TModel model)		| update data with id `id` with supplied `model` object asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *int* Delete(TKey id)									| delete data with id `id`. Return an integer indicating number of affected record. |
| *Task<int>* DeleteAsync(TKey id)						| delete data with id `id` asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *bool* IsExists(TKey id)								| check if data with specified `id` exists. Return boolean. |

#### classes

##### BaseService
Abstract class for service implementation, implements `Com.Moonlay.NetCore.Lib.Services.IService`.

| Properties											| Description |
| ------------------------------------					|  --------    |
| *IServiceProvider* ServiceProvider					| Service Provider, _read only_ |
| *Microsoft.EntityFrameworCore.DbContext* DbContext	| DbContext, _read only_ |


| Methods												| Description |
| ------------------------------------					| --------    |
| BaseService(IServiceProvider)							| `Constructor`, receive `System.IServiceProvider` as parameter. |
| *IEnumerable<object>* Get()							| return all object. |
| *object* Get(object id)								| return object with specified `id` object. |
| *int* Create(object model)							| create/persist supplied `model` object. Return an integer indicating number of affected record. |
| *Task<int>* CreateAsync(object model)					| create/persist supplied `model` object asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *int* Update(object id, object model)					| update data with id `id` with supplied `model` object. Return an integer indicating number of affected record. |
| *Task<int>* UpdateAsync(object id, object model)		| update data with id `id` with supplied `model` object asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *int* Delete(object id)								| delete data with id `id`. Return an integer indicating number of affected record. |
| *Task<int>* DeleteAsync(object id)					| delete data with id `id` asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *bool* IsExists(object id)							| check if data with specified `id` exists. Return boolean. |


##### BaseService<TDbContext, TModel, TKey>
Generic abstract class for service implementation, inherited from `Com.Moonlay.NetCore.Lib.Services.BaseService` implements `Com.Moonlay.NetCore.Lib.Services.IService<TModel, TKey>`.

|Generic Parameter										| Description|
|-----------------										| -----------|
|TDbContext												| DbContext type, should be inherited from `Microsoft.EntityFrameworkCore.DbContext` |
|TModel													| Model type, should be an implementation of `Com.Moonlay.Models.IEntity`|
|TKey													| Key type, should be an implementation of `System.IConvertible` |

| Method												| Description |
| ------------------------------------					| --------    |
| BaseService(IServiceProvider)							| `Constructor`, receive `System.IServiceProvider` as parameter. |
| *IEnumerable<TModel>* Get()							| return all object. |
| *object* Get(TKey id)									| return object with specified `id` object. |
| *int* Create(TModel model)							| create/persist supplied `model` object. Return an integer indicating number of affected record. Throws `Com.Moonlay.NetCore.Lib.Services.ServiceValidationException` when validation is not valid. |
| *Task<int>* CreateAsync(TModel model)					| create/persist supplied `model` object asynchronously. Return Task which resolve an integer indicating number of affected record. Throws `Com.Moonlay.NetCore.Lib.Services.ServiceValidationException` when validation is not valid. |
| *int* Update(TKey id, TModel model)					| update data with id `id` with supplied `model` object. Return an integer indicating number of affected record. Throws `Com.Moonlay.NetCore.Lib.Services.ServiceValidationException` when validation is not valid. |
| *Task<int>* UpdateAsync(TKey id, TModel model)		| update data with id `id` with supplied `model` object asynchronously. Return Task which resolve an integer indicating number of affected record. Throws `Com.Moonlay.NetCore.Lib.Services.ServiceValidationException` when validation is not valid. |
| *int* Delete(TKey id)									| delete data with id `id`. Return an integer indicating number of affected record. |
| *Task<int>* DeleteAsync(TKey id)						| delete data with id `id` asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *bool* IsExists(TKey id)								| check if data with specified `id` exists. Return boolean. |
| virtual *void* OnCreating(TModel model)				| custom code before create data. |
| virtual *void* OnUpdating(TKey id, TModel model)		| custom code before update data. |
| virtual *void* OnDeleting(TModel model)				| custom code before delete data. |


##### StandardEntityService<TDbContext, TModel>
Generic standard implementation of BaseService class, inherited from `Com.Moonlay.NetCore.Lib.Services.BaseService<TDbContext, TModel, int>`. This class is enough for most implementations. This class use `int` for generic parameter `TKey` because `Com.Moonlay.Models.StandardEntity` use type `int` as `Key` type.

|Generic Parameter										| Description|
|-----------------										| -----------|
|TDbContext												| DbContext type, should be inherited from `Microsoft.EntityFrameworkCore.DbContext` |
|TModel													| Model type, should be instance of `Com.Moonlay.Models.StandardEntity` and implements `System.ComponentModel.DataAnnotations.IValidatableObject`|

| Method												| Description |
| ------------------------------------					| --------    |
| StandardEntityService(IServiceProvider)							| `Constructor`, receive `System.IServiceProvider` as parameter, usually get injected from DI system.|
| *IEnumerable<TModel>* Get()							| return all object. |
| *object* Get(int id)									| return object with specified `id` object. |
| *int* Create(TModel model)							| create/persist supplied `model` object. Return an integer indicating number of affected record. |
| *Task<int>* CreateAsync(TModel model)					| create/persist supplied `model` object asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *int* Update(int id, TModel model)					| update data with id `id` with supplied `model` object. Return an integer indicating number of affected record. |
| *Task<int>* UpdateAsync(int id, TModel model)			| update data with id `id` with supplied `model` object asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *int* Delete(int id)									| delete data with id `id`. Return an integer indicating number of affected record. |
| *Task<int>* DeleteAsync(int id)						| delete data with id `id` asynchronously. Return Task which resolve an integer indicating number of affected record. |
| *bool* IsExists(int id)								| check if data with specified `id` exists. Return boolean. |
| virtual *void* OnCreating(TModel model)				| custom code before create data. write audit data by default. |
| virtual *void* OnUpdating(TKey id, TModel model)		| custom code before update data. write audit data by default. |
| virtual *void* OnDeleting(TModel model)				| custom code before delete data. implements soft delete by default. |
