﻿// 
// QuerySelectResult.cs
// 
// Copyright (c) 2017 Couchbase, Inc All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 

using System;
using Couchbase.Lite.Query;

namespace Couchbase.Lite.Internal.Query
{
    internal sealed class QuerySelectResult : ISelectResultAs, ISelectResultFrom
    {
        internal readonly IExpression Expression;
        private string _alias;
        private string _from = String.Empty;

        internal string ColumnName
        {
            get {
                if (_alias != null) {
                    return _alias;
                }

                QueryTypeExpression keyPathExpr = Expression as QueryTypeExpression;
                var columnName = keyPathExpr?.ColumnName;
                if(columnName == null) {
                    return null;
                }

                return $"{_from}{columnName}".TrimEnd('.');
            }
        }
        
        public QuerySelectResult(IExpression expression)
        {
            Expression = expression;
        }

        public ISelectResult As(string alias)
        {
            _alias = alias;
            return this;
        }

        public ISelectResult From(string alias)
        {
            _from = $"{alias}.";
            (Expression as QueryTypeExpression).From(alias);
            return this;
        }
    }
}