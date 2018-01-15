
/*<FILE_LICENSE>
* NFX (.NET Framework Extension) Unistack Library
* Copyright 2003-2018 Agnicore Inc. portions ITAdapter Corp. Inc.
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

/* NFX by ITAdapter
 * Originated: 2006.01
 * Revision: NFX 5.0  2018.1.15
 * Based on zXing / Apache 2.0; See NOTICE and CHANGES for attribution
 */

namespace NFX.Media.TagCodes.QR
{
  public sealed class QRCorrectionLevel
  {
    public static readonly QRCorrectionLevel L = new QRCorrectionLevel("L", 0, 0x01); // up to 7%
    public static readonly QRCorrectionLevel M = new QRCorrectionLevel("M", 1, 0x00); // up to 15%
    public static readonly QRCorrectionLevel Q = new QRCorrectionLevel("Q", 2, 0x03); // up to 25%
    public static readonly QRCorrectionLevel H = new QRCorrectionLevel("H", 3, 0x02); // up to 30%

    public static readonly QRCorrectionLevel[] LEVELS = new [] { L, M, Q, H};


    private QRCorrectionLevel (string name, int ordinal, int markerBits)
    {
      Name = name;
      Ordinal = ordinal;
      MarkerBits = markerBits;
    }

    public readonly string Name;
    public readonly int Ordinal;
    public readonly int MarkerBits;


    public override string ToString() => $"QRECL({Name})";
  }

}
