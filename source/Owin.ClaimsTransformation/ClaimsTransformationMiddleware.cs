/*
 * Copyright 2014, 2015 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Microsoft.Owin;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace IdentityModel.Owin
{
    public class ClaimsTransformationMiddleware
    {
        readonly ClaimsTransformationOptions _options;
        readonly AppFunc _next;

        public ClaimsTransformationMiddleware(AppFunc next, ClaimsTransformationOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);
            
            if (context.Authentication != null && 
                context.Authentication.User != null &&
                context.Authentication.User.Identity != null)
            {
                var transformedPrincipal = await _options.ClaimsTransformation(context.Authentication.User);
                context.Authentication.User = new ClaimsPrincipal(transformedPrincipal);
            }

            await _next(env);
        }
    }
}