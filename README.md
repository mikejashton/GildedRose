# About GildedRose
This program automates the inventory management process. It accepts an properly formatted input file and outputs  
an adjusted inventory to another (output) file. The input file must be formatted according to these rules:  

1. The file must have one inventory item per line
2. The file must be delimited by spaces. 
3. The column order is fixed: Name, Sell in period, Quality metric
4. The name may contain spaces. The program will always look for the last two columns and treat them as their respective values

The program requires the following command line arguments:
1. The input file name
2. The output filename

Example: GildedRose inputfilename outputfilename

The program will return 0 on success and any errors will be output to std out.

## File format
An example input file is shown below:

Backstage passes 9 2\
Sulfuras 2 2

Both lines are parsed as follows

| Name | Sell In Period | Quality |
--- | --- | --- |
| Backstage passes | 9 | 2 |
| Sulfuras | 2 | 2 |

The name is currently used as a key to indicate the product type. The list of valid values are:  
* "Aged Brie"
* "Backstage passes"
* "Sulfuras"
* "Normal Item"
* "Conjured"

If an invalid name is specified, the item will be replaced by "NO SUCH ITEM" when outputting the updated inventory.

## Improvements
1. The main entry point needs to be refactored to allow for correct integration testing and to improve readability. 
I really hate how big it is right now. This would be the first thing I did if I had more time.
2. The directory structure needs to be tidied up to make clear the purpose behind the various classes
3. A couple of the factory objects are not DRY. I would like to resolve this.
4. If this is likely to be extended further I would be nice to introduce dependency injection
5. Make use a factory method/mock item in the unit tests to remove the allocation of Items
6. More comments to describe the correct behaviour of the  unit tests
7. Tidy up visibility of class members (make things internal that don't need to be public).
  
