﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace DynClient
{
    interface IAddressRetriever
    {
        IPAddress GetAddress();

    }
}
