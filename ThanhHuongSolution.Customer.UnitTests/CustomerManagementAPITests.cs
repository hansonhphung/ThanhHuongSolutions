﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThanhHuongSolution.Customer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.UnitTests;

namespace ThanhHuongSolution.Customer.Handler
{
    [TestClass]
    public class CustomerManagementAPITests
    {
        private IObjectContainer _objectContainer;

        [TestInitialize]
        public void Init()
        {
            _objectContainer = UnitTestHelper.SetupKernelBootstraper();

            
        }

        [TestMethod]
        public async Task CustomerManagementAPITest()
        {
            var api = _objectContainer.Get<ICustomerManagementAPI>();

            var result = await api.CreateCustomer(new FrameworkParamInput<Domain.Model.CustomerInfo>(new CustomerInfo()));
        }
    }
}