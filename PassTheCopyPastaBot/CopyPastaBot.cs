using System;
using System.Collections.Generic;
using System.Text;

namespace PassTheCopyPastaBot
{
    public class CopyPastaBot
    {
        private string token;
        private CopyPasta copyPasta;

        public CopyPastaBot(string token)
        {
            this.token = token;
            this.copyPasta = new CopyPasta();
        }

    }
}
