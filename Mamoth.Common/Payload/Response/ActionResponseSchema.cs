﻿using System;

namespace Mamoth.Common.Payload.Response
{
    public class ActionResponseSchema : ActionResponseBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}