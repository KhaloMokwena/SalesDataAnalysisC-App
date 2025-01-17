# Enhanced Sales Data Analysis Console Application

## Overview

This project is a C# console application designed to help you analyze sales data from a CSV file. It calculates important metrics such as total sales, average sales, the highest sale amount, and more. The app is easy to use and comes with built-in error handling to ensure smooth operation even if the CSV file is missing or the user makes an invalid selection.

In addition, the application includes unit tests to verify that the business logic is working correctly.

## Features

The application supports the following functionalities:

- **Total Sales Amount**: Calculate the total sales from all transactions.
- **Average Sales Amount**: Calculate the average sales per transaction.
- **Highest Sale Amount**: Find the transaction with the highest sales amount.
- **Sales Grouped by Product**: Group the sales by product and calculate total and average sales for each.
- **Salesperson with Maximum Sales**: Find the salesperson who made the highest total sales.
- **Salesperson Sales Count**: Display the number of sales made by each salesperson.
- **Error Handling**: Gracefully handles errors such as missing files, invalid data formats, and invalid user input.

## CSV File Structure

The application reads data from a CSV file named `sales_data.csv`. The file contains the following columns:

- **TransactionId**: A unique identifier for each transaction (string).
- **Amount**: The sales amount for the transaction (double).
- **Product**: The name of the product sold (string).
- **SalesPersonId**: The identifier for the salesperson who made the sale (integer).

### Example CSV Data:

```csv
TransactionId,Amount,Product,SalesPersonId
T001,100.50,Widget A,101
T002,200.75,Widget B,102
T003,150.00,Widget A,101
T004,300.00,Widget C,103
T005,300.00,Widget C,104
T006,200.75,Widget B,104
```
# Enhanced Sales Data Analysis Console Application

## Overview

This project is a C# console application designed to perform enhanced sales data analysis. The application reads sales data from a CSV file (`sales_data.csv`), processes it, and calculates various metrics based on user input. It allows the user to select from different analysis options such as total sales, average sales, highest sale amount, and more.

The application is built to be user-friendly with error handling for common issues such as missing or malformed CSV files, invalid data formats, and incorrect user inputs. This project provides a clear example of how to handle file input/output, perform data analysis, and interact with the user through a simple console interface.

## Features

The application supports the following functionalities:
- **Total Sales Amount**: Calculate the total sales from all transactions.
- **Average Sales Amount**: Calculate the average sales per transaction.
- **Highest Sale Amount**: Find the transaction with the highest sales amount.
- **Sales Grouped by Product**: Group the sales by product and calculate total and average sales for each.
- **Salesperson with Maximum Sales**: Find the salesperson who made the highest total sales.
- **Salesperson Sales Count**: Display the number of sales made by each salesperson.
- **Error Handling**: Gracefully handles errors such as missing files, invalid data formats, and invalid user input.

## CSV File Structure

The application reads data from a CSV file named `sales_data.csv`. The file contains the following columns:

- **TransactionId**: A unique identifier for each transaction (string).
- **Amount**: The sales amount for the transaction (double).
- **Product**: The name of the product sold (string).
- **SalesPersonId**: The identifier for the salesperson who made the sale (integer).


 ### Example CSV Data:

```csv
TransactionId,Amount,Product,SalesPersonId
T001,100.50,Widget A,101
T002,200.75,Widget B,102
T003,150.00,Widget A,101
T004,300.00,Widget C,103
T005,300.00,Widget C,104
T006,200.75,Widget B,104
```

### Example CSV Data when opened in a csv compatible App:

| TransactionId | Amount | Product   | SalesPersonId |
|---------------|--------|-----------|---------------|
| T001          | 100.50 | Widget A  | 101           |
| T002          | 200.75 | Widget B  | 102           |
| T003          | 150.00 | Widget A  | 101           |
| T004          | 300.00 | Widget C  | 103           |
| T005          | 300.00 | Widget C  | 104           |
| T006          | 200.75 | Widget B  | 104           |


## How to Run the Application

1. Open the project in Visual Studio or another C# IDE.
2. Make sure the `sales_data.csv` file is in the same directory as the application.
3. Build and run the console application.
4. Follow the on-screen menu to select different analysis options.

## Assumptions

- The `sales_data.csv` file is structured correctly and follows the format mentioned above.
- The user provides valid input when selecting menu options (e.g., entering numbers).
- Error handling is implemented for missing files, invalid data formats, and incorrect user inputs.

## Error Handling

The application is designed to handle the following errors:

- **Missing CSV file**: If the file is not found, the application will inform the user and display the called file path.
- **Invalid CSV data**: If any line in the CSV has missing or malformed data, the application will skip it and continue processing valid lines.
- **Invalid user input**: If the user enters an invalid choice (e.g., non-numeric input when selecting an option), the application will prompt them to try again.


