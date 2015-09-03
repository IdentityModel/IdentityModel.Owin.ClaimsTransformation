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

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Owin;

namespace Owin
{
    /// <summary>
    /// AppBuilder extensions for the claims transformation middleware
    /// </summary>
    public static class ClaimsTransformationMiddlewareExtensions
    {
        /// <summary>
        /// Adds the claims transformation middleware to the OWIN pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="transformation">The transformation function.</param>
        /// <returns></returns>
        public static IAppBuilder UseClaimsTransformation(this IAppBuilder app, Func<ClaimsPrincipal, Task<ClaimsPrincipal>> transformation)
        {
            return app.UseClaimsTransformation(new ClaimsTransformationOptions
                {
                    ClaimsTransformation = transformation
                });
        }

        /// <summary>
        /// Adds the claims transformation middleware to the OWIN pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">options</exception>
        public static IAppBuilder UseClaimsTransformation(this IAppBuilder app, ClaimsTransformationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            app.Use<ClaimsTransformationMiddleware>(options);
            return app;
        }
    }
}