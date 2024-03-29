﻿namespace IotaWalletNet.Domain.Common.Models.Coin
{
    /// <summary>
    /// IRC30 Native Token Metadata Schema
    ///  "required": [ "standard", "name", "symbol", "decimals" ]
    /// </summary>
    public class NativeTokenIRC30
    {
        public NativeTokenIRC30(string name, string symbol, uint decimals)
        {
            Name = name;
            Symbol = symbol;
            Decimals = decimals;
        }

        /// <summary>
        /// The IRC standard of the token metadata
        /// </summary>
        public string Standard { get; } = "IRC30";

        /// <summary>
        /// The human-readable name of the native token
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The human-readable description of the token
        /// </summary>
        public string? Description { get; set; }

        public NativeTokenIRC30 SetDescription(string description)
        {
            Description = description;
            return this;
        }

        /// <summary>
        /// The symbol/ticker of the token
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Number of decimals the token uses (divide the token amount by decimals to get its user representation)
        /// </summary>
        public uint Decimals { get; }

        /// <summary>
        /// URL pointing to more resources about the token
        /// </summary>
        public string? Url { get; set; }

        public NativeTokenIRC30 SetUrl(string url)
        {
            Url = url;
            return this;
        }

        public string? LogoUrl { get; set; }

        public NativeTokenIRC30 SetLogoUrl(string logoUrl)
        {
            LogoUrl = logoUrl;
            return this;
        }

        public string? Logo { get; set; }

        public NativeTokenIRC30 SetLogo(string logoFilePath)
        {
            if (File.Exists(logoFilePath))
            {
                byte[] fileBytes = File.ReadAllBytes(logoFilePath);
                Logo = Convert.ToHexString(fileBytes);
                return this;
            }

            throw new FileNotFoundException($"{logoFilePath} not found.");
        }


    }
}
