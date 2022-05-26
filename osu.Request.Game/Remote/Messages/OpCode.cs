﻿// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

namespace osu.Request.Game.Remote.Messages;

public enum IncomingOpCode
{
    SERVER_ERROR = 0,
    SERVER_BEATMAPSET_NONEXISTENT = 1,
    SERVER_USER_NONEXISTENT = 2,

    AUTH_ALL_BEATMAPSET_BANS = 203,
    AUTH_ALL_USER_BANS = 204,

    BEATMAPSET_REQUEST = 300,
    BEATMAPSET_BAN = 301,
    BEATMAPSET_UNBAN = 302,

    USER_BAN = 400,
    USER_UNBAN = 401
}

public enum OutgoingOpCode
{
    REQUEST_BEATMAPSET_BAN = 101,
    REQUEST_BEATMAPSET_UNBAN = 102,
    REQUEST_USER_BAN = 103,
    REQUEST_USER_UNBAN = 104
}
