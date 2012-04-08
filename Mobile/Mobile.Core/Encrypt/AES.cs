﻿using System;
using System.Text;

namespace Mobile.Core.Encrypt
{
    /// <summary>
    /// Performs AES (Advanced Encryption Standard) cryptography
    /// see: http://msdn.microsoft.com/msdnmag/issues/03/11/AES/
    /// </summary>
    public class AES
    {
        #region KeySize enum

        public enum KeySize
        {
            Bits128,
            Bits192,
            Bits256
        } ;

        #endregion

        private const int BlockSize = 16; // block size for encrypt and decrypt. should always be 16
        private byte[,] iSbox; // inverse Substitution box 
        private byte[] key; // the seed key. size will be 4 * keySize from ctor.
        // key size, in bits, for construtor

        private int Nb; // block size in 32-bit words.  Always 4 for AES.  (128 bits).
        private int Nk; // key size in 32-bit words.  4, 6, 8.  (128, 192, 256 bits).
        private int Nr; // number of rounds. 10, 12, 14.

        private byte[,] Rcon; // Round constants.
        private byte[,] Sbox; // Substitution box
        private byte[,] State; // State matrix
        private byte[,] w; // key schedule array. 

        #region Initialize

        /// <summary>
        /// Initializes the AES engine
        /// </summary>
        /// <param name="keySize">Size of the key</param>
        /// <param name="Password">the password</param>
        public AES(KeySize keySize, string Password)
        {
            // convert the password to a byte array, then call the other init func
            byte[] keyBytes = ConvertStringToByteArray(Password);
            Init(keySize, keyBytes);
        }

        /// <summary>
        /// Initializes the AES engine
        /// </summary>
        /// <param name="keySize">Size of the key</param>
        /// <param name="keyBytes">the password, in a byte array</param>
        public AES(KeySize keySize, byte[] keyBytes)
        {
            Init(keySize, keyBytes);
        } // Aes constructor

        private void Init(KeySize keySize, byte[] keyBytes)
        {
            int i = 0;
            int keyLen = 0, kbLen = 0;
            byte nextByte = 1;

            try
            {
                SetNbNkNr(keySize);

                // set the key
                key = new byte[Nk * 4]; // 16, 24, 32 bytes

                // if the password is the right size, just copy the array
                if (key.Length == keyBytes.Length)
                    keyBytes.CopyTo(key, 0);
                else // password is different size, so manually copy
                {
                    // get the key lengths
                    keyLen = key.Length;
                    kbLen = keyBytes.Length;

                    // manuually add the password
                    for (i = 0; i < keyLen; i++)
                    {
                        // make sure we can use the keyBytes
                        if (i < kbLen)
                            key[i] = keyBytes[i];
                        else // we need to add some extra bytes
                            key[i] = nextByte++;
                    }
                }

                // build the two matrixes
                BuildSbox();
                BuildInvSbox();
                BuildRcon();
                KeyExpansion(); // expand the seed key into a key schedule and store in w
            }

            catch (Exception excep)
            {
                AES_ShowError(excep, "Init");
            }
        }

        #endregion // Initialize

        #region Public Encrypt and Decrypt

        /// <summary>
        /// Encrypts the input string
        /// </summary>
        /// <param name="input">the string to encrypt  -   
        /// Note: the base encryptiong algorithm uses a byte array, but it's not always
        /// possible to convert the byte array to a string, using a 1:1 conversion.
        /// Instead, we will convert each byte to a string, and add a space. So,
        /// when you are encrypting a string, the return result will always be larger
        /// in length than the original. When you are decrypting a string, the return
        /// result will always be smaller in length than the original encrypted string.
        /// </param>
        public string Encrypt(string input)
        {
            string rText = "";
            int i = 0, sLen = 0;

            try
            {
                // convert the string to a byte
                byte[] bInput = ConvertStringToByteArray(input);

                // encrypt
                byte[] bText = Encrypt(bInput);

                // convert the byte array to a string
                sLen = bText.Length;
                for (i = 0; i < sLen; i++)
                    rText += bText[i].ToString().Trim() + " ";
            }

            catch (Exception excep)
            {
                AES_ShowError(excep, "Encrypt");
            }

            // return the text
            return rText.Trim();
        }

        /// <summary>
        /// Encrypts the input byte array
        /// </summary>
        /// <param name="input">the byte array to encrypt</param>
        public byte[] Encrypt(byte[] input)
        {
            int i = 0, iLen = input.Length;
            byte[] output = new byte[0];
            byte[] newInput;
            byte[] inBuffer = new byte[BlockSize];
            byte[] buffer = new byte[BlockSize];
            int count = 0;

            try
            {
                // we need to resize the arrays so they are 16 byte blocks
                count = GetArraySize(input.Length);
                output = new byte[count];
                newInput = new byte[count];

                // copy the data from input to newInput
                Array.Copy(input, 0, newInput, 0, input.Length);

                // we need to send the cipher function 16 bytes at a time to encrypt
                for (i = 0; i < iLen; i = i + BlockSize)
                {
                    // copy the input into the input buffer array
                    Array.Copy(newInput, i, inBuffer, 0, BlockSize); // copy all 16 bytes

                    // encrypt this block
                    Array.Copy(Cipher(inBuffer), 0, output, i, BlockSize);
                }
            }

            catch (Exception excep)
            {
                AES_ShowError(excep, "Encrypt");
            }

            // return the byte array
            return output;
        }

        /// <summary>
        /// Decrypts the input string
        /// </summary>
        /// <param name="input">the string to decrypt  -  
        /// Note: the base encryption algorithm uses a byte array, but it's not always
        /// possible to convert the byte array to a string, using a 1:1 conversion.
        /// Instead, we converted each byte to a string, and added a space. So,
        /// when you are encrypting a string, the return result will always be larger
        /// in length than the original. When you are decrypting a string, the return
        /// result will always be smaller in length than the original encrypted string.
        /// </param>
        public string Decrypt(string input)
        {
            string rText = "";
            int i = 0, sLen = 0;

            try
            {
                // convert the string to a byte array
                string[] tByte = input.Split(' ');
                sLen = tByte.Length;
                byte[] bInput = new byte[sLen];
                for (i = 0; i < sLen; i++)
                    bInput[i] = Convert.ToByte(tByte[i]);

                // decrypt and convert to the byte array to a string
                rText = ConvertByteArrayToString(Decrypt(bInput));
            }

            catch (Exception excep)
            {
                AES_ShowError(excep, "Decrypt");
            }

            // return
            return rText;
        }

        /// <summary>
        /// Decrypts the input byte array
        /// </summary>
        /// <param name="input">the byte array to decrypt</param>
        public byte[] Decrypt(byte[] input)
        {
            int i = 0, iLen = input.Length;
            byte[] inBuffer = new byte[BlockSize];
            byte[] buffer = new byte[BlockSize];
            byte[] output = new byte[input.Length];
            //int count = 0;

            try
            {
                // we need to send the cipher function 16 bytes at a time to encrypt
                for (i = 0; i < iLen; i = i + BlockSize)
                {
                    // copy the input into the input buffer array
                    Array.Copy(input, i, inBuffer, 0, BlockSize); // copy all 16 bytes

                    // decrypt this block
                    Array.Copy(InvCipher(inBuffer), 0, output, i, BlockSize);
                }
            }

            catch (Exception excep)
            {
                AES_ShowError(excep, "Decrypt");
            }

            // return the byte array
            return output;
        }

        #endregion // Public Encrypt and Decrypt

        #region Private Encrypt and Decrypt

        /// <summary>
        /// Encrypt the data in a 16 bit block. returns:
        /// 16 byte block of encrypted characters
        /// </summary>
        /// <param name="input">16 byte block of characters to encrypt</param>
        /// <param name="retCount">the number of bytes actually used</param>
        /// <returns>16 byte block of encrypted characters</returns>
        private byte[] Cipher(byte[] input) // encipher 16-bit input
        {
            byte[] output = new byte[16];

            try
            {
                // state = input
                State = new byte[4, Nb]; // always [4,4]
                for (int i = 0; i < (4 * Nb); ++i)
                {
                    State[i % 4, i / 4] = input[i];
                }

                AddRoundKey(0);

                for (int round = 1; round <= (Nr - 1); ++round) // main round loop
                {
                    SubBytes();
                    ShiftRows();
                    MixColumns();
                    AddRoundKey(round);
                } // main round loop

                SubBytes();
                ShiftRows();
                AddRoundKey(Nr);

                // output = state
                for (int i = 0; i < (4 * Nb); ++i)
                {
                    output[i] = State[i % 4, i / 4];
                }
            }

            catch (Exception excep)
            {
                AES_ShowError(excep, "Cipher");
            }

            return output;
        } // Cipher()

        /// <summary>
        /// Decrypts a 16 byte block of text
        /// </summary>
        /// <param name="input">16 byte block to decrypt</param>
        /// <returns>16 byte block of decrypted bytes</returns>
        private byte[] InvCipher(byte[] input) // decipher 16-bit input
        {
            byte[] output = new byte[16];

            try
            {
                // state = input
                State = new byte[4, Nb]; // always [4,4]
                for (int i = 0; i < (4 * Nb); ++i)
                {
                    State[i % 4, i / 4] = input[i];
                }

                AddRoundKey(Nr);

                for (int round = Nr - 1; round >= 1; --round) // main round loop
                {
                    InvShiftRows();
                    InvSubBytes();
                    AddRoundKey(round);
                    InvMixColumns();
                } // end main round loop for InvCipher

                InvShiftRows();
                InvSubBytes();
                AddRoundKey(0);

                // output = state
                for (int i = 0; i < (4 * Nb); ++i)
                {
                    output[i] = State[i % 4, i / 4];
                }
            }

            catch (Exception excep)
            {
                AES_ShowError(excep, "InvCipher");
            }

            return output;
        } // InvCipher()

        #endregion // Private Encrypt and Decrypt

        #region AES Tables and Work Body

        private void SetNbNkNr(KeySize keySize)
        {
            Nb = 4; // block size always = 4 words = 16 bytes = 128 bits for AES

            if (keySize == KeySize.Bits128)
            {
                Nk = 4; // key size = 4 words = 16 bytes = 128 bits
                Nr = 10; // rounds for algorithm = 10
            }
            else if (keySize == KeySize.Bits192)
            {
                Nk = 6; // 6 words = 24 bytes = 192 bits
                Nr = 12;
            }
            else if (keySize == KeySize.Bits256)
            {
                Nk = 8; // 8 words = 32 bytes = 256 bits
                Nr = 14;
            }
        } // SetNbNkNr()

        private void BuildSbox()
        {
            Sbox = new byte[16, 16]
                {
                    // populate the Sbox matrix
                    /* 0     1     2     3     4     5     6     7     8     9     a     b     c     d     e     f */
                    /*0*/
                    {0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76},
                    /*1*/
                    {0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0},
                    /*2*/
                    {0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15},
                    /*3*/
                    {0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75},
                    /*4*/
                    {0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84},
                    /*5*/
                    {0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf},
                    /*6*/
                    {0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8},
                    /*7*/
                    {0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2},
                    /*8*/
                    {0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73},
                    /*9*/
                    {0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb},
                    /*a*/
                    {0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79},
                    /*b*/
                    {0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08},
                    /*c*/
                    {0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a},
                    /*d*/
                    {0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e},
                    /*e*/
                    {0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf},
                    /*f*/
                    {0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16}
                };
        } // BuildSbox() 

        private void BuildInvSbox()
        {
            iSbox = new byte[16, 16]
                {
                    // populate the iSbox matrix
                    /* 0     1     2     3     4     5     6     7     8     9     a     b     c     d     e     f */
                    /*0*/
                    {0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb},
                    /*1*/
                    {0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb},
                    /*2*/
                    {0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e},
                    /*3*/
                    {0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25},
                    /*4*/
                    {0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92},
                    /*5*/
                    {0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84},
                    /*6*/
                    {0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06},
                    /*7*/
                    {0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b},
                    /*8*/
                    {0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73},
                    /*9*/
                    {0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e},
                    /*a*/
                    {0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b},
                    /*b*/
                    {0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4},
                    /*c*/
                    {0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f},
                    /*d*/
                    {0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef},
                    /*e*/
                    {0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61},
                    /*f*/
                    {0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d}
                };
        } // BuildInvSbox()

        private void BuildRcon()
        {
            Rcon = new byte[11, 4]
                {
                    {0x00, 0x00, 0x00, 0x00},
                    {0x01, 0x00, 0x00, 0x00},
                    {0x02, 0x00, 0x00, 0x00},
                    {0x04, 0x00, 0x00, 0x00},
                    {0x08, 0x00, 0x00, 0x00},
                    {0x10, 0x00, 0x00, 0x00},
                    {0x20, 0x00, 0x00, 0x00},
                    {0x40, 0x00, 0x00, 0x00},
                    {0x80, 0x00, 0x00, 0x00},
                    {0x1b, 0x00, 0x00, 0x00},
                    {0x36, 0x00, 0x00, 0x00}
                };
        } // BuildRcon()

        private void AddRoundKey(int round)
        {
            for (int r = 0; r < 4; ++r)
            {
                for (int c = 0; c < 4; ++c)
                {
                    State[r, c] = (byte)((int)State[r, c] ^ (int)w[(round * 4) + c, r]);
                }
            }
        } // AddRoundKey()

        private void SubBytes()
        {
            for (int r = 0; r < 4; ++r)
            {
                for (int c = 0; c < 4; ++c)
                {
                    State[r, c] = Sbox[(State[r, c] >> 4), (State[r, c] & 0x0f)];
                }
            }
        } // SubBytes

        private void InvSubBytes()
        {
            for (int r = 0; r < 4; ++r)
            {
                for (int c = 0; c < 4; ++c)
                {
                    State[r, c] = iSbox[(State[r, c] >> 4), (State[r, c] & 0x0f)];
                }
            }
        } // InvSubBytes

        private void ShiftRows()
        {
            byte[,] temp = new byte[4, 4];
            for (int r = 0; r < 4; ++r) // copy State into temp[]
            {
                for (int c = 0; c < 4; ++c)
                {
                    temp[r, c] = State[r, c];
                }
            }

            for (int r = 1; r < 4; ++r) // shift temp into State
            {
                for (int c = 0; c < 4; ++c)
                {
                    State[r, c] = temp[r, (c + r) % Nb];
                }
            }
        } // ShiftRows()

        private void InvShiftRows()
        {
            byte[,] temp = new byte[4, 4];
            for (int r = 0; r < 4; ++r) // copy State into temp[]
            {
                for (int c = 0; c < 4; ++c)
                {
                    temp[r, c] = State[r, c];
                }
            }
            for (int r = 1; r < 4; ++r) // shift temp into State
            {
                for (int c = 0; c < 4; ++c)
                {
                    State[r, (c + r) % Nb] = temp[r, c];
                }
            }
        } // InvShiftRows()

        private void MixColumns()
        {
            byte[,] temp = new byte[4, 4];
            for (int r = 0; r < 4; ++r) // copy State into temp[]
            {
                for (int c = 0; c < 4; ++c)
                {
                    temp[r, c] = State[r, c];
                }
            }

            for (int c = 0; c < 4; ++c)
            {
                State[0, c] = (byte)((int)gfmultby02(temp[0, c]) ^ (int)gfmultby03(temp[1, c]) ^
                                      (int)gfmultby01(temp[2, c]) ^ (int)gfmultby01(temp[3, c]));
                State[1, c] = (byte)((int)gfmultby01(temp[0, c]) ^ (int)gfmultby02(temp[1, c]) ^
                                      (int)gfmultby03(temp[2, c]) ^ (int)gfmultby01(temp[3, c]));
                State[2, c] = (byte)((int)gfmultby01(temp[0, c]) ^ (int)gfmultby01(temp[1, c]) ^
                                      (int)gfmultby02(temp[2, c]) ^ (int)gfmultby03(temp[3, c]));
                State[3, c] = (byte)((int)gfmultby03(temp[0, c]) ^ (int)gfmultby01(temp[1, c]) ^
                                      (int)gfmultby01(temp[2, c]) ^ (int)gfmultby02(temp[3, c]));
            }
        } // MixColumns

        private void InvMixColumns()
        {
            byte[,] temp = new byte[4, 4];
            for (int r = 0; r < 4; ++r) // copy State into temp[]
            {
                for (int c = 0; c < 4; ++c)
                {
                    temp[r, c] = State[r, c];
                }
            }

            for (int c = 0; c < 4; ++c)
            {
                State[0, c] = (byte)((int)gfmultby0e(temp[0, c]) ^ (int)gfmultby0b(temp[1, c]) ^
                                      (int)gfmultby0d(temp[2, c]) ^ (int)gfmultby09(temp[3, c]));
                State[1, c] = (byte)((int)gfmultby09(temp[0, c]) ^ (int)gfmultby0e(temp[1, c]) ^
                                      (int)gfmultby0b(temp[2, c]) ^ (int)gfmultby0d(temp[3, c]));
                State[2, c] = (byte)((int)gfmultby0d(temp[0, c]) ^ (int)gfmultby09(temp[1, c]) ^
                                      (int)gfmultby0e(temp[2, c]) ^ (int)gfmultby0b(temp[3, c]));
                State[3, c] = (byte)((int)gfmultby0b(temp[0, c]) ^ (int)gfmultby0d(temp[1, c]) ^
                                      (int)gfmultby09(temp[2, c]) ^ (int)gfmultby0e(temp[3, c]));
            }
        } // InvMixColumns

        private static byte gfmultby01(byte b)
        {
            return b;
        }

        private static byte gfmultby02(byte b)
        {
            if (b < 0x80)
                return (byte)(int)(b << 1);
            else
                return (byte)((int)(b << 1) ^ (int)(0x1b));
        }

        private static byte gfmultby03(byte b)
        {
            return (byte)((int)gfmultby02(b) ^ (int)b);
        }

        private static byte gfmultby09(byte b)
        {
            return (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^
                           (int)b);
        }

        private static byte gfmultby0b(byte b)
        {
            return (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^
                           (int)gfmultby02(b) ^
                           (int)b);
        }

        private static byte gfmultby0d(byte b)
        {
            return (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^
                           (int)gfmultby02(gfmultby02(b)) ^
                           (int)(b));
        }

        private static byte gfmultby0e(byte b)
        {
            return (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^
                           (int)gfmultby02(gfmultby02(b)) ^
                           (int)gfmultby02(b));
        }

        private void KeyExpansion()
        {
            w = new byte[Nb * (Nr + 1), 4]; // 4 columns of bytes corresponds to a word

            for (int row = 0; row < Nk; ++row)
            {
                w[row, 0] = key[4 * row];
                w[row, 1] = key[4 * row + 1];
                w[row, 2] = key[4 * row + 2];
                w[row, 3] = key[4 * row + 3];
            }

            byte[] temp = new byte[4];

            for (int row = Nk; row < Nb * (Nr + 1); ++row)
            {
                temp[0] = w[row - 1, 0];
                temp[1] = w[row - 1, 1];
                temp[2] = w[row - 1, 2];
                temp[3] = w[row - 1, 3];

                if (row % Nk == 0)
                {
                    temp = SubWord(RotWord(temp));

                    temp[0] = (byte)((int)temp[0] ^ (int)Rcon[row / Nk, 0]);
                    temp[1] = (byte)((int)temp[1] ^ (int)Rcon[row / Nk, 1]);
                    temp[2] = (byte)((int)temp[2] ^ (int)Rcon[row / Nk, 2]);
                    temp[3] = (byte)((int)temp[3] ^ (int)Rcon[row / Nk, 3]);
                }
                else if (Nk > 6 && (row % Nk == 4))
                {
                    temp = SubWord(temp);
                }

                // w[row] = w[row-Nk] xor temp
                w[row, 0] = (byte)((int)w[row - Nk, 0] ^ (int)temp[0]);
                w[row, 1] = (byte)((int)w[row - Nk, 1] ^ (int)temp[1]);
                w[row, 2] = (byte)((int)w[row - Nk, 2] ^ (int)temp[2]);
                w[row, 3] = (byte)((int)w[row - Nk, 3] ^ (int)temp[3]);
            } // for loop
        } // KeyExpansion()

        private byte[] SubWord(byte[] word)
        {
            byte[] result = new byte[4];
            result[0] = Sbox[word[0] >> 4, word[0] & 0x0f];
            result[1] = Sbox[word[1] >> 4, word[1] & 0x0f];
            result[2] = Sbox[word[2] >> 4, word[2] & 0x0f];
            result[3] = Sbox[word[3] >> 4, word[3] & 0x0f];
            return result;
        }

        private byte[] RotWord(byte[] word)
        {
            byte[] result = new byte[4];
            result[0] = word[1];
            result[1] = word[2];
            result[2] = word[3];
            result[3] = word[0];
            return result;
        }

        #endregion // AES Tables and Work Body

        #region Byte and String Conversions

        /// <summary>
        /// Converts a string to a byte array
        /// </summary>
        /// <param name="StrToConvert">the string to convert</param>
        private byte[] ConvertStringToByteArray(string StrToConvert)
        {
            //return ASCIIEncoding.ASCII.GetBytes(StrToConvert);
            return UTF8Encoding.UTF8.GetBytes(StrToConvert);
        }

        /// <summary>
        /// Converts a byte array to a string
        /// </summary>
        /// <param name="ByteToConvert"></param>
        /// <returns></returns>
        private string ConvertByteArrayToString(byte[] ByteToConvert)
        {
            //string tempStr = "";
            //for (int i = 0; i < ByteToConvert.Length; i++)
            //{
            //    // do not convert 0
            //    if (ByteToConvert[i] > 0)
            //        tempStr += Convert.ToChar(ByteToConvert[i]);
            //}

            //return tempStr;
            return UTF8Encoding.UTF8.GetString(ByteToConvert);
        }

        #endregion // Byte and String Conversions

        #region Misc Functions

        /// <summary>
        /// Display an error message
        /// </summary>
        /// <param name="excep">the exception</param>
        /// <param name="FunctionName">the name of the function that caused the error</param>
        private void AES_ShowError(Exception excep, string FunctionName)
        {
            string errMsg = "";
            string nl = "\r\n";

            try
            {
                // build the error string
                errMsg += "An error has ocurred." + nl;
                errMsg += "Function: AES." + FunctionName.Trim() + nl;
                errMsg += "Source: " + excep.Source + nl;
                errMsg += "Error Description: " + nl + excep.Message;

                // return the message
            }
            catch
            {
            }
        }

        /// <summary>
        /// Returns the size of the array needed to meet BlockSize
        /// </summary>
        /// <param name="ArrayLen">the length of the current array</param>
        private int GetArraySize(int ArrayLen)
        {
            // if this is divisible by blocksize, return arraylen
            if ((BlockSize % ArrayLen) == 0)
                return ArrayLen;

            // return the new array size
            return (((ArrayLen / BlockSize) + 1) * BlockSize);
        }

        #endregion // Misc Functions
    }
}
