namespace Approve.RuleBase
{
    using System;
    using System.ComponentModel;

    public class RuntimeRegistryLicense : License
    {
        private Type type;

        public RuntimeRegistryLicense(Type type)
        {
            if (type == null)
            {
                throw new NullReferenceException("The licensed type reference may not be null.");
            }
            this.type = type;
        }

        public override void Dispose()
        {
        }

        public override string LicenseKey
        {
            get
            {
                return this.type.GUID.ToString();
            }
        }
    }
}

