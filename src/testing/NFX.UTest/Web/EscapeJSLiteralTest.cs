﻿/*<FILE_LICENSE>
* NFX (.NET Framework Extension) Unistack Library
* Copyright 2003-2018 ITAdapter Corp. Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
</FILE_LICENSE>*/
using System;
using NFX.Scripting;

namespace NFX.UTest.Web
{
  [Runnable]
  public class EscapeJSLiteralTest
  {
    [Run]
    public void Empty()
    {
      Aver.AreEqual(null, MiscUtils.EscapeJSLiteral(null));
      Aver.AreEqual("", MiscUtils.EscapeJSLiteral(""));
      Aver.AreEqual("   ", MiscUtils.EscapeJSLiteral("   "));
    }

    [Run]
    public void Quotes()
    {
      Aver.AreEqual(@"Mc\x27Cloud", MiscUtils.EscapeJSLiteral("Mc'Cloud"));
      Aver.AreEqual(@"Mc\x22Cloud", MiscUtils.EscapeJSLiteral("Mc\"Cloud"));
      Aver.AreEqual(@"Mc\x22\x27Cloud", MiscUtils.EscapeJSLiteral("Mc\"'Cloud"));

    }

    [Run]
    public void Script()
    {
      Aver.AreEqual(@"not \x3C\x2Fscript\x3E the end", MiscUtils.EscapeJSLiteral("not </script> the end"));
    }

    [Run]
    public void RN()
    {
      Aver.AreEqual(@"not \x0D \x0A the end", MiscUtils.EscapeJSLiteral("not \r \n the end"));
    }

    [Run]
    public void Various()
    {
      Aver.AreEqual(@"not\x27s \x22\x26amp;\x22 the\x5Cs\x2F end", MiscUtils.EscapeJSLiteral(@"not's ""&amp;"" the\s/ end"));
    }
  }
}
