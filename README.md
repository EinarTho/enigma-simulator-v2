# Enigma Machine Simulator v2

A comprehensive C# implementation of the historical Enigma machine, featuring improved architecture, better error handling, and enhanced user experience.

## Features

- **Authentic Enigma Implementation**: Uses historically accurate rotor wirings, reflectors, and stepping mechanisms
- **Modular Architecture**: Clean separation of concerns with interfaces and proper abstraction
- **Comprehensive Error Handling**: Robust input validation and exception handling
- **Improved User Interface**: Better console UI with clear prompts and feedback
- **Extensible Design**: Easy to add new rotor types, reflectors, or plugboard configurations

## Architecture Improvements

### Before
- Tightly coupled components
- Debug output in production code
- Magic numbers and hardcoded values
- Poor error handling
- Inefficient string operations

### After
- **Interfaces**: `IComponent`, `IRotor` for better abstraction
- **Factory Pattern**: `EnigmaMachineFactory` for clean object creation
- **Configuration Management**: `EnigmaConfiguration` class for centralized settings
- **Proper Error Handling**: Comprehensive validation and meaningful error messages
- **Clean Architecture**: Separation of concerns between UI, business logic, and configuration

## Components

### Core Classes
- `EnigmaMachine`: Main orchestrator class
- `Rotor`: Implements rotor logic with proper stepping mechanism
- `Plugboard`: Handles letter pair connections
- `Reflector`: Manages reflector connections
- `Component`: Base class for all Enigma components

### Configuration
- `EnigmaConfiguration`: Centralized configuration with historical rotor wirings
- `EnigmaMachineFactory`: Factory for creating properly configured machines

### User Interface
- `EnigmaUI`: Improved console interface with better user experience

## Usage

1. **Build the project**:
   ```bash
   dotnet build
   ```

2. **Run the simulator**:
   ```bash
   dotnet run --project Enigma
   ```

3. **Follow the prompts**:
   - Enter rotor positions (0-25)
   - Type text to encode
   - View results
   - Choose to continue or exit

## Historical Accuracy

This implementation includes:
- **Standard Rotors I-V**: With authentic wiring configurations
- **Standard Reflectors A-C**: With proper bidirectional mappings
- **Double-stepping Mechanism**: Correct rotor advancement logic
- **Plugboard Support**: Letter pair connections (currently default configuration)

## Technical Improvements

### Code Quality
- ✅ Removed debug console output
- ✅ Fixed syntax errors
- ✅ Improved naming conventions
- ✅ Added comprehensive error handling
- ✅ Used StringBuilder for efficient string operations

### Architecture
- ✅ Implemented interfaces for better abstraction
- ✅ Added factory pattern for object creation
- ✅ Centralized configuration management
- ✅ Improved separation of concerns

### Functionality
- ✅ Fixed rotor rotation logic
- ✅ Implemented proper plugboard functionality
- ✅ Added input validation
- ✅ Enhanced user experience

## Project Structure

```
enigma-simulator-v2/
├── Enigma/                 # Main console application
├── EnigmaComponents/       # Core Enigma machine logic
├── EnigmaComponents.Tests/ # Unit tests
└── UI/                    # User interface components
```

## Testing

Run the test suite:
```bash
dotnet test
```

## Future Enhancements

- Add support for custom rotor wirings
- Implement ring settings
- Add file I/O for batch processing
- Create a graphical user interface
- Add encryption/decryption modes
- Support for different Enigma variants (M3, M4, etc.) 
