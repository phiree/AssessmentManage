using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YunChuang.IdAccess.DomainModel;
using YunChuang.IdAccess.DomainModel.Repository;

namespace YunChuang.IdAccess.Infrastructure.NHibernate.Repository
{
	public class RepositoryMembership:NHRepositoryBase<Membership,string>, IRepositoryMembership
	{
	}
}
