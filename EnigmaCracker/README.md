# Enigma Machine Cracker

A console application that attempts to crack Enigma-encoded messages using various cryptographic techniques.

## Features

### 🔍 **Multiple Cracking Strategies**
- **Brute Force**: Try all possible rotor combinations and positions
- **Known Rotor Types**: Assume rotor types are known, try different positions
- **Known Rotor Positions**: Assume positions are known, try different rotor types
- **Quick Test**: Limited attempts for testing purposes

### 🧠 **Intelligent Scoring**
- **Word Frequency Analysis**: Scores based on common English words
- **Bigram Analysis**: Considers letter pair frequencies
- **Trigram Analysis**: Considers three-letter combinations
- **Normalized Scoring**: Results normalized by message length

### ⚡ **Performance Optimizations**
- **Async Processing**: Non-blocking operations for better responsiveness
- **Configurable Limits**: Set maximum attempts to control execution time
- **Progress Tracking**: Real-time feedback on cracking progress

## Usage

### Building the Project
```bash
dotnet build
```

### Running the Cracker
```bash
dotnet run --project EnigmaCracker
```

### Example Session
```
=== Enigma Machine Cracker ===

Enter the encoded message to crack: ILBDAAMTAZ

Select cracking strategy:
1. Brute force (try all rotor combinations)
2. Known rotor types (try different positions)
3. Known rotor positions (try different types)
4. Quick test (limited attempts)
Enter choice (1-4): 2

Enter maximum attempts (default 100,000): 10000

Starting crack attempt...
Message: ILBDAAMTAZ
Strategy: KnownRotorTypes
Max attempts: 10,000

=== Cracking Results ===
✅ CRACKED SUCCESSFULLY!
Decoded message: HELLOWORLD
Configuration: Rotors: I(0) II(0) III(0) Reflector: B
Attempts made: 1,000
Time taken: 0.15 seconds
```

## How It Works

### 1. **Message Processing**
- Filters out non-letter characters
- Converts to uppercase for consistency
- Validates input format

### 2. **Configuration Generation**
- Creates Enigma machines with different settings
- Tries various rotor combinations and positions
- Tests different reflector configurations

### 3. **Decoding Attempts**
- Encodes the message through each configuration
- Scores the resulting output using language statistics
- Tracks the best-scoring result

### 4. **Scoring Algorithm**
The cracker uses a multi-factor scoring system:
- **Word Score**: Checks for common English words
- **Bigram Score**: Analyzes letter pair frequencies
- **Trigram Score**: Considers three-letter patterns
- **Final Score**: Normalized combination of all factors

### 5. **Success Criteria**
- High confidence threshold (configurable)
- Best guess tracking for failed attempts
- Detailed result reporting

## Technical Details

### **Supported Rotor Types**
- Rotors I, II, III, IV, V (historical Enigma configurations)
- Reflectors A, B, C
- All 26 rotor positions (A-Z)

### **Performance Characteristics**
- **Known Rotor Types**: ~17,576 attempts (26³ positions)
- **Brute Force**: ~1,404,608 attempts (5×4×3×3×26³)
- **Quick Test**: ~1,000 attempts (limited combinations)

### **Scoring Thresholds**
- **Success**: Score > 0.7 (high confidence)
- **Good Guess**: Score > 0.3 (moderate confidence)
- **Poor Guess**: Score < 0.1 (low confidence)

## Limitations

1. **Computational Complexity**: Brute force approach is computationally expensive
2. **Language Dependency**: Optimized for English text
3. **Message Length**: Shorter messages are harder to crack
4. **Plugboard**: Current implementation doesn't handle plugboard settings

## Future Enhancements

- [ ] Plugboard support
- [ ] Ring setting configuration
- [ ] Multi-language support
- [ ] Parallel processing
- [ ] Machine learning-based scoring
- [ ] Known plaintext attacks
- [ ] Crib-based cracking

## Dependencies

- **EnigmaComponents**: Core Enigma machine implementation
- **UI**: User interface components
- **.NET 8.0**: Runtime framework

## Contributing

This project is part of the larger Enigma simulator suite. Contributions are welcome for:
- Improved scoring algorithms
- Additional cracking strategies
- Performance optimizations
- Better language support 