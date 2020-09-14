using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEntrySample.Behaviors
{
    public class MyCustomValidator : SmartBehavior
    {
        public MyCustomValidator(string ErrorText) : base(ErrorText) { }

        public override bool IsTextValid(string text)
        {
            throw new NotImplementedException();
        }
    }
}
