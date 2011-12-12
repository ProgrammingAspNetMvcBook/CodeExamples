using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    public static class AssertException
    {

        public static void Throws<TException>(Action actionThatThrowsAnException, string message = null)
            where TException : Exception
        {
            try
            {
                actionThatThrowsAnException();
            }
            catch (TException)
            {
                // Succeeded!
                return;
            }

            if(message == null)
                Assert.Fail();
            else
                Assert.Fail(message);
        }

    }
}
