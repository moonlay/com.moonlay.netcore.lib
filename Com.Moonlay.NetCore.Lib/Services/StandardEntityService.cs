using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Moonlay.NetCore.Lib.Service
{
    public class StandardEntityService<TDbContext, TModel> : BaseEFService<TDbContext, TModel, int>
          where TDbContext : DbContext
          where TModel : StandardEntity, IValidatableObject
    {
        public StandardEntityService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public override void OnCreating(TModel model)
        {
            var nowUtc = DateTime.UtcNow;
            var agent = string.Empty;
            var actor = string.Empty;

            model._CreatedBy = model._LastModifiedBy = actor;
            model._CreatedAgent = model._LastModifiedAgent = agent;
            model._CreatedUtc = model._LastModifiedUtc = nowUtc;
        }

        public override void OnUpdating(int id, TModel model)
        {
            var nowUtc = DateTime.UtcNow;
            var agent = string.Empty;
            var actor = string.Empty;

            model._LastModifiedBy = actor;
            model._LastModifiedAgent = agent;
            model._LastModifiedUtc = nowUtc;
        }

        public override void OnDeleting(TModel model)
        {
            var nowUtc = DateTime.UtcNow;
            var agent = string.Empty;
            var actor = string.Empty;

            OnUpdating(model.Id, model);

            model._IsDeleted = true;
            model._DeletedBy = actor;
            model._DeletedAgent = agent;
            model._DeletedUtc = nowUtc;
        }
    }
}
