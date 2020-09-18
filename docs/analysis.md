# Analysis

## Investigation
### Introduction to Client
Mr Kevin Ashley is the managing director of Ashley Quality Improvement Services, a company that provides consultancy services for drug manufacture in the pharmaceutical industry. His role involves frequent travel around the UK and the EU, with schedules often changing to suit client needs. As such, he makes frequent use of public transport methods such as air and rail travel. However, during this travel he is often disrupted due to delays (particularly in the UK) - and as such has asked myself to attempt a program that would be able to dynamically reroute his journey as a result of delays.

### Outline of the Problem
When travelling by train in the UK, Mr Ashley's journeys often involve at least one connection between services. However, disruption on the rail network can result in him missing his connection, and having to wait at a connecting station. Due to the nature of his work and lack of facilities at train stations, a missed connection typically results in a disruption to his workflow; potentially reducing billable hours. Furthermore, due to the complex nature of his journeys, a missed connection early in the journey typically results in having to find alternative connections later in the journey - causing greater disruption to his workflow. 

For example, Mr Ashley regularly travels from his home station of Ulverston to Chester to visit a client. This journey involves a minimum of two changes; from the Ulverston train to the West Coast Main Line (WCML), then from the WCML to Chester. There are various routings available from the WCML to Chester - via Warrington, and via Crewe. The earliest arrival time on this route depends on available trains in the timetable. As such, a missed connection onto a WCML service can mean a routing that was originally quicker via Crewe is quicker via Warrington - however, this is harder to identify during a journey without recalculating the entire route again manually.

Furthermore, recalculating the route manually typically does not take into account factors such as connection times from one service to another. In the case of the Warrington/Crewe problem, a train running 15 minute

Therefore, Mr Ashley is seeking a solution that informs him of any disruption to his journey in real time; and if the disruption is so great as to cause a missed connection, recalculate his route based on the fastest possible routing.

### Interview with Client
**What system do you currently use for travelling during disruption?**

**What benefits does your current system have?**

**What are the drawbacks of your current system?**

**Are there any particular features you feel are essential to a new system (must haves)?**

**Are there any features that you feel would improve the system but do not require? (nice to haves)**

**How would you envisage using this new system? (web app / mobile app?)**

**Do you envisage a need to connect this system with other corporate services?**

**Is there anything else you would like to add?**

### Current System
Currently, Mr Ashley books his journeys on the 'Trainline' app prior to the journey - but typically does not have time to check the status of his journey. As such, delays are often realised upon arriving at the station or during the journey. If the delay is sufficient enough to cause a missed connection, many of his journeys have the potential to have quicker arrival at the destination via a different routing - meaning that he often replans his whole journey from that point. However, when involved in disruption, this does not always enable him to achieve the earliest arrival at his destination due to the real time nature of delays.

### User Requirements

## Analysis of Investigation

### Data Flow Diagrams

### Research

## Proposed Solution

### Data Volumes