using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class DALException:Exception
    {
        public DALException(string message):base(message)
        {

        }
        public class DomainNotFundException:DALException
        {
            public DomainNotFundException(string message):base(message)
            {

            }
        }
        public class DomainValidationFundException : DALException
        {
            public DomainValidationFundException(string message) : base(message)
            {

            }
        }
        public class DomainExpiredException : DALException
        {
            public DomainExpiredException(string message) : base(message)
            {

            }
        }
        public class DomainInternalException : DALException
        {
            public DomainInternalException(string message) : base(message)
            {

            }
        }
    }
}
