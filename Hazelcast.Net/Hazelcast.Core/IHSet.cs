// Copyright (c) 2008-2018, Hazelcast, Inc. All Rights Reserved.
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

namespace Hazelcast.Core
{
    /// <summary>
    ///     Concurrent, distributed implementation of ISet
    /// </summary>
    /// <remarks>
    ///     <b>
    ///         This class is <i>not</i> a general-purpose <tt>ISet</tt> implementation! While this class implements
    ///         the <tt>Set</tt> interface, it intentionally violates <tt>Set's</tt> general contract, which mandates the
    ///         use of the <tt>Equals</tt> method when comparing objects. Instead of the equals method this implementation
    ///         compares the serialized byte version of the objects.
    ///     </b>
    /// </remarks>
    public interface IHSet<T> : /*ISet<E>,*/ IHCollection<T>
    {
    }
}