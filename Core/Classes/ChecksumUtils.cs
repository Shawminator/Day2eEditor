using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public static class ChecksumUtils
{
    /// <summary>
    /// Computes the SHA256 checksum for a file and returns it in "sha256-" format.
    /// </summary>
    public static string ComputeSha256Checksum(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException("The specified file does not exist.", filePath);

        using (var sha256 = SHA256.Create())
        {
            // Read the file in chunks to avoid memory issues with large files
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hashBytes = sha256.ComputeHash(stream);

                // Convert byte array to hex string and return as "sha256-" format
                return "sha256-" + BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
    /// <summary>
    /// Normalizes a checksum to ensure it uses the "sha256-" prefix and is lowercase.
    /// </summary>
    public static string NormalizeChecksum(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            throw new ArgumentException("Checksum is null or empty.");

        string hex = raw.StartsWith("sha256-", StringComparison.OrdinalIgnoreCase)
            ? raw.Substring(7)
            : raw;

        if (hex.Length != 64 || !Regex.IsMatch(hex, @"\A[0-9a-fA-F]{64}\z"))
            throw new FormatException("Invalid SHA256 hash format.");

        return "sha256-" + hex.ToLowerInvariant();
    }

    /// <summary>
    /// Validates that a checksum string is in the expected "sha256-<64 hex chars>" format.
    /// </summary>
    public static void ValidateChecksumFormat(string checksum)
    {
        if (!Regex.IsMatch(checksum ?? "", @"^sha256\-[0-9a-fA-F]{64}$"))
            throw new FormatException("Checksum must be in the format 'sha256-<64 hex characters>'.");
    }

    /// <summary>
    /// Computes a SHA256 checksum for a byte array and returns it in "sha256-..." format.
    /// </summary>
    public static string ComputeSha256(byte[] data)
    {
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(data);
        return "sha256-" + BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// Verifies that the checksum of the data matches the expected value.
    /// </summary>
    public static bool VerifyChecksum(byte[] data, string expected)
    {
        string actual = ComputeSha256(data);
        string normalizedExpected = NormalizeChecksum(expected);
        return string.Equals(actual, normalizedExpected, StringComparison.OrdinalIgnoreCase);
    }
}