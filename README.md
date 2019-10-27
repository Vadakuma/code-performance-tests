# code-performance-tests

Used unity version: Unity 2019.1.14f

# Some code performance tests

* Foreach VS For
* Components number and gameobject with deep child hierarchy
* Ling.FindLastIndex vs custom implementation
* Get Field VS Get Property
* ContainsKey VS TryGetValue from Dictionary (searching only existed key)
* Using ToStrig()
* Using method inlining
* Allocate item from list in for loop
* Out VS Return
* Remove action for List vs Dictionary

With this tests you can make decision which optimizations worth paying attention in your case or which not. 

PS. Some tests shows only special case, for example **ContainsKey VS TryGetValue** can make similar performance resalts if you trying to find item which wasn't represent in a dictionary.
