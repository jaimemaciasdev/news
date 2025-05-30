# News API (.NET 9)

An ASP.NET Core Web API that fetches and serves top stories from the Hacker News API, with in-memory caching and configurable settings.

## Features

- Fetches top stories from Hacker News using their public API
- Caches story data and story IDs in memory for improved performance
- Configurable cache duration and API base URL
- Simple REST API endpoint for clients

## Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Internet access (to reach the Hacker News API)

## Getting Started

### 1. Clone the repository

- git clone [news](https://github.com/jaimemaciasdev/news) cd news

### 2. Configuration

- Edit `appsettings.json` to adjust the Hacker News API base URL or cache duration:

### 3. Build and Run

- dotnet build dotnet run --project news

- The API will be available at `https://localhost:5001` or `http://localhost:5000` by default.

## API Usage

### Get Top Stories

**Request:**
GET /api/news/top?n=5
- `n` (optional): Number of top stories to return (default: 10)

**Response:**
[ { "title": "Example Story", "url": "https://example.com/story", "by": "author", "time": 1717000000, "score": 123, "descendants": 45 }, ... ]

## Changes with more time/enhancements

- Add authentication/authorization
- Rate limiting
- Separate into Domain, Infrastructure, Application, Presentation projects/layers
- Add unit tests
- Create dtos for the services instead of using the same model

## Asumptions
- After analyzing HackerNews Api it returns the beststories in descending order by score, so that is why I do not fetch the full list of story details and then sort it