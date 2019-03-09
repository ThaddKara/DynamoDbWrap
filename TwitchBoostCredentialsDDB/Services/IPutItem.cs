using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBoostCredentialsDDB.Services
{
	public interface IPutItem
	{
		Task AddItem(string ApiKey, string TwitchName);
		Task AddTest(string ApiKey, string TwitchName);
	}
}
