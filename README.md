# Checkout Kata

### Assumptions
Discounts can apply multiple times per product type within the same checkout session.

### Basic issues and refactor potential
* No proper exeption handling or logging.
* No IoC - The 'Checkout' should be provided with services such as offer service, or repository if there were one.
* Used abstract class with enum to indicate transaction type. Could have used 'is' keyword but favoured performance.
* Those apples are a rip off!
