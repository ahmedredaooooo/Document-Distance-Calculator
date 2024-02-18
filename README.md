# Document Distance Calculation

## Assignment for Algorithm Analysis and Design

### Description

This utility computes the distance between two documents, D1 and D2, in a case-insensitive manner. It's designed for applications such as searching for similar documents, detecting duplicates (e.g., Wikipedia mirrors), and plagiarism detection.

### Definitions

- **Word:** A sequence of alphanumeric characters only.
- **Document:** A sequence of words, ignoring spaces, punctuation, etc.
- **Case Insensitivity:** Treats all uppercase letters as if they are lowercase, ensuring consistency in comparisons.

### Approach

1. Define distance in terms of shared words.
2. Represent each document D as a vector, where D[w] denotes the number of occurrences of word w.
3. Calculate the distance d(D1, D2) as the angle between the two vectors.
   - 0°: Documents are identical.
   - 90°: No common words.

### Optimization

Store the computed answers to save time on subsequent executions.

### Usage

Input two documents, D1 and D2. Execute the calculation to obtain the distance between them.

### Applications

- Document similarity search
- Duplicate detection
- Plagiarism detection
