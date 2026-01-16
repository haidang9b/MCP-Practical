# Student Management MCP Server

This is a **Model Context Protocol (MCP)** server built with .NET 9.0, designed to manage student records and perform basic analysis on student scores.

## Overview

The purpose of this application is to demonstrate how to expose C# tools and resources via the Model Context Protocol. It allows AI agents and other MCP clients to interact with a student database (in-memory) to perform queries and modifications.

## Features

-   **Student Management**: Add and update student records with scores in Math, Science, and English.
-   **Data Analysis**: Calculate average scores and identify top-performing students.
-   **MCP Integration**: Fully compliant with the Model Context Protocol, exposing tools for easy consumption.

## Available Tools

The server exposes the following tools:

### 1. `GetStudentsAsync`
Retrieves the full list of students currently stored in the system.

### 2. `CalculateAverageAsync`
Calculates the average score across all subjects for a specific student.
-   **Arguments**: `name` (string) - The name of the student.

### 3. `GetBestStudentAsync`
Identifies the student with the highest global average score across all subjects.

### 4. `AddStudentAsync`
Adds a new student to the database.
-   **Arguments**:
    -   `id` (int): Unique identifier.
    -   `name` (string): Student's full name.
    -   `math` (int): Score in Math.
    -   `science` (int): Score in Science.
    -   `english` (int): Score in English.

### 5. `UpdateStudentAsync`
Updates an existing student's details.
-   **Arguments**:
    -   `id` (int): The ID of the student to update.
    -   `name` (string): New name.
    -   `math` (int): New Math score.
    -   `science` (int): New Science score.
    -   `english` (int): New English score.

## Getting Started

### Prerequisites
-   .NET 9.0 SDK or later.

### Running the Server
1.  Navigate to the project directory:
    ```bash
    cd MCPPractical
    ```
2.  Build the project:
    ```bash
    dotnet build
    ```
3.  Run the application:
    ```bash
    dotnet run
    ```
    The server will start and listen for MCP connections (configuration depends on `appsettings.json` and program setup).

## Project Structure

-   `Tools/StudentMcpTools.cs`: Contains the implementation of the MCP tools.
-   `Services/StudentStorage.cs`: A thread-safe singleton service for in-memory data storage.
-   `Models/Student.cs`: The data model representing a student.
