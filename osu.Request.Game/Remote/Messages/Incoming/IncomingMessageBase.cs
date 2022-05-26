// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using Newtonsoft.Json;

namespace osu.Request.Game.Remote.Messages.Incoming;

public class IncomingMessageBase
{
    [JsonProperty("op")]
    public IncomingOpCode Op;
}
