using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBoostCredentialsDDB.Services
{
	public interface IDeleteItem
	{
		Task Delete(string ApiKey, string Name);
		Task DeleteDoc(string ApiKey);
	}
}
