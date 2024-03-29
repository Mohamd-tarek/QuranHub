﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuranHub.DAL.Migrations.IdentityData
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CoverPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Religion = table.Column<int>(type: "int", nullable: false),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Online = table.Column<bool>(type: "bit", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayListsInfo",
                columns: table => new
                {
                    PlayListInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThumbnailImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfVideos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayListsInfo", x => x.PlayListInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Verses",
                columns: table => new
                {
                    VerseId = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verses", x => x.VerseId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    FollowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FollowerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowedId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<int>(type: "int", nullable: false),
                    Shares = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.FollowId);
                    table.ForeignKey(
                        name: "FK_Followed_QuranHubUsers",
                        column: x => x.FollowedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Following_QuranHubUsers",
                        column: x => x.FollowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuranHubUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_QuranHubUserId",
                        column: x => x.QuranHubUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrivacySettings",
                columns: table => new
                {
                    PrivacySettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllowFollow = table.Column<bool>(type: "bit", nullable: false),
                    AllowComment = table.Column<bool>(type: "bit", nullable: false),
                    AllowShare = table.Column<bool>(type: "bit", nullable: false),
                    AppearInSearch = table.Column<bool>(type: "bit", nullable: false),
                    QuranHubUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivacySettings", x => x.PrivacySettingId);
                    table.ForeignKey(
                        name: "FK_PrivacySettings_AspNetUsers_QuranHubUserId",
                        column: x => x.QuranHubUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideosInfo",
                columns: table => new
                {
                    VideoInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThumbnailImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false),
                    PlayListInfoId = table.Column<int>(type: "int", nullable: false),
                    ReactsCount = table.Column<int>(type: "int", nullable: false),
                    CommentsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideosInfo", x => x.VideoInfoId);
                    table.ForeignKey(
                        name: "FK_VideosInfo_PlayListsInfo_PlayListInfoId",
                        column: x => x.PlayListInfoId,
                        principalTable: "PlayListsInfo",
                        principalColumn: "PlayListInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuranHubUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VerseId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReactsCount = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    VideoInfoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_QuranHubUserId",
                        column: x => x.QuranHubUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Verses_VerseId",
                        column: x => x.VerseId,
                        principalTable: "Verses",
                        principalColumn: "VerseId");
                    table.ForeignKey(
                        name: "FK_Comments_VideosInfo_VideoInfoId",
                        column: x => x.VideoInfoId,
                        principalTable: "VideosInfo",
                        principalColumn: "VideoInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TargetUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seen = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    CommentNotification_CommentId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    PostCommentId = table.Column<int>(type: "int", nullable: true),
                    FollowId = table.Column<int>(type: "int", nullable: true),
                    ReactId = table.Column<int>(type: "int", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    CommentReactId = table.Column<int>(type: "int", nullable: true),
                    PostCommentReactNotification_PostId = table.Column<int>(type: "int", nullable: true),
                    PostCommentReactId = table.Column<int>(type: "int", nullable: true),
                    PostCommentCommentId = table.Column<int>(type: "int", nullable: true),
                    VideoInfoId = table.Column<int>(type: "int", nullable: true),
                    VideoInfoCommentReactId = table.Column<int>(type: "int", nullable: true),
                    VideoInfoCommentCommentId = table.Column<int>(type: "int", nullable: true),
                    PostReactNotification_PostId = table.Column<int>(type: "int", nullable: true),
                    PostReactId = table.Column<int>(type: "int", nullable: true),
                    ShareId = table.Column<int>(type: "int", nullable: true),
                    PostShareNotification_PostId = table.Column<int>(type: "int", nullable: true),
                    PostShareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_CommentNotification_Comment_CommentId",
                        column: x => x.CommentNotification_CommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentReactNotification_Comment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Notifications_Comments_PostCommentCommentId",
                        column: x => x.PostCommentCommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Notifications_Comments_VideoInfoCommentCommentId",
                        column: x => x.VideoInfoCommentCommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Notifications_Follows_FollowId",
                        column: x => x.FollowId,
                        principalTable: "Follows",
                        principalColumn: "FollowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCommentNotification_PostComment_CommentId",
                        column: x => x.PostCommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Source_QuranHubUsers",
                        column: x => x.SourceUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Target_QuranHubUsers",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoInfoCommentReactNotification_VideoInfo_VideoInfoId",
                        column: x => x.VideoInfoId,
                        principalTable: "VideosInfo",
                        principalColumn: "VideoInfoId");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Privacy = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuranHubUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VerseId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReactsCount = table.Column<int>(type: "int", nullable: false),
                    CommentsCount = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    SharesCount = table.Column<int>(type: "int", nullable: true),
                    PostShareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_QuranHubUserId",
                        column: x => x.QuranHubUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Verses_VerseId",
                        column: x => x.VerseId,
                        principalTable: "Verses",
                        principalColumn: "VerseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reacts",
                columns: table => new
                {
                    ReactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    QuranHubUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    PostCommentReact_PostId = table.Column<int>(type: "int", nullable: true),
                    PostCommentCommentId = table.Column<int>(type: "int", nullable: true),
                    VideoInfoId = table.Column<int>(type: "int", nullable: true),
                    VideoInfoCommentCommentId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    VideoInfoReact_VideoInfoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reacts", x => x.ReactId);
                    table.ForeignKey(
                        name: "FK_PostReact_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId");
                    table.ForeignKey(
                        name: "FK_Reacts_AspNetUsers_QuranHubUserId",
                        column: x => x.QuranHubUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reacts_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Reacts_Comments_PostCommentCommentId",
                        column: x => x.PostCommentCommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Reacts_Comments_VideoInfoCommentCommentId",
                        column: x => x.VideoInfoCommentCommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Reacts_Posts_PostCommentReact_PostId",
                        column: x => x.PostCommentReact_PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reacts_VideosInfo_VideoInfoId",
                        column: x => x.VideoInfoId,
                        principalTable: "VideosInfo",
                        principalColumn: "VideoInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoInfoReact_VideoInfo_VideoInfoId",
                        column: x => x.VideoInfoReact_VideoInfoId,
                        principalTable: "VideosInfo",
                        principalColumn: "VideoInfoId");
                });

            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    ShareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuranHubUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    ShareablePostPostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => x.ShareId);
                    table.ForeignKey(
                        name: "FK_Shares_AspNetUsers_QuranHubUserId",
                        column: x => x.QuranHubUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shares_Posts_ShareablePostPostId",
                        column: x => x.ShareablePostPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_QuranHubUserId",
                table: "Comments",
                column: "QuranHubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VerseId",
                table: "Comments",
                column: "VerseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VideoInfoId",
                table: "Comments",
                column: "VideoInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowedId",
                table: "Follows",
                column: "FollowedId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowerId",
                table: "Follows",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_QuranHubUserId",
                table: "Notes",
                column: "QuranHubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CommentId",
                table: "Notifications",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CommentNotification_CommentId",
                table: "Notifications",
                column: "CommentNotification_CommentId",
                unique: true,
                filter: "[CommentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CommentReactId",
                table: "Notifications",
                column: "CommentReactId",
                unique: true,
                filter: "[CommentReactId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FollowId",
                table: "Notifications",
                column: "FollowId",
                unique: true,
                filter: "[FollowId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostCommentCommentId",
                table: "Notifications",
                column: "PostCommentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostCommentId",
                table: "Notifications",
                column: "PostCommentId",
                unique: true,
                filter: "[PostCommentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostCommentReactId",
                table: "Notifications",
                column: "PostCommentReactId",
                unique: true,
                filter: "[PostCommentReactId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostCommentReactNotification_PostId",
                table: "Notifications",
                column: "PostCommentReactNotification_PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostId",
                table: "Notifications",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostReactId",
                table: "Notifications",
                column: "PostReactId",
                unique: true,
                filter: "[PostReactId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostReactNotification_PostId",
                table: "Notifications",
                column: "PostReactNotification_PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostShareId",
                table: "Notifications",
                column: "PostShareId",
                unique: true,
                filter: "[PostShareId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostShareNotification_PostId",
                table: "Notifications",
                column: "PostShareNotification_PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReactId",
                table: "Notifications",
                column: "ReactId",
                unique: true,
                filter: "[ReactId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ShareId",
                table: "Notifications",
                column: "ShareId",
                unique: true,
                filter: "[ShareId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SourceUserId",
                table: "Notifications",
                column: "SourceUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TargetUserId",
                table: "Notifications",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_VideoInfoCommentCommentId",
                table: "Notifications",
                column: "VideoInfoCommentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_VideoInfoCommentReactId",
                table: "Notifications",
                column: "VideoInfoCommentReactId",
                unique: true,
                filter: "[VideoInfoCommentReactId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_VideoInfoId",
                table: "Notifications",
                column: "VideoInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostShareId",
                table: "Posts",
                column: "PostShareId",
                unique: true,
                filter: "[PostShareId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_QuranHubUserId",
                table: "Posts",
                column: "QuranHubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_VerseId",
                table: "Posts",
                column: "VerseId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivacySettings_QuranHubUserId",
                table: "PrivacySettings",
                column: "QuranHubUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_CommentId",
                table: "Reacts",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_PostCommentCommentId",
                table: "Reacts",
                column: "PostCommentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_PostCommentReact_PostId",
                table: "Reacts",
                column: "PostCommentReact_PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_PostId",
                table: "Reacts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_QuranHubUserId",
                table: "Reacts",
                column: "QuranHubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_VideoInfoCommentCommentId",
                table: "Reacts",
                column: "VideoInfoCommentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_VideoInfoId",
                table: "Reacts",
                column: "VideoInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_VideoInfoReact_VideoInfoId",
                table: "Reacts",
                column: "VideoInfoReact_VideoInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Shares_QuranHubUserId",
                table: "Shares",
                column: "QuranHubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shares_ShareablePostPostId",
                table: "Shares",
                column: "ShareablePostPostId");

            migrationBuilder.CreateIndex(
                name: "IX_VideosInfo_PlayListInfoId",
                table: "VideosInfo",
                column: "PlayListInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReactNotification_CommentReact_CommentReactId",
                table: "Notifications",
                column: "CommentReactId",
                principalTable: "Reacts",
                principalColumn: "ReactId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReactNotification_PostCommentReact_PostCommentReactId",
                table: "Notifications",
                column: "PostCommentReactId",
                principalTable: "Reacts",
                principalColumn: "ReactId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReactNotification_PostReact_PostReactId",
                table: "Notifications",
                column: "PostReactId",
                principalTable: "Reacts",
                principalColumn: "ReactId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReactNotification_React_ReactId",
                table: "Notifications",
                column: "ReactId",
                principalTable: "Reacts",
                principalColumn: "ReactId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoInfoCommentReactNotification_VideoInfoCommentReact_VideoInfoCommentReactId",
                table: "Notifications",
                column: "VideoInfoCommentReactId",
                principalTable: "Reacts",
                principalColumn: "ReactId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentNotification_Post_PostId",
                table: "Notifications",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReactNotification_Post_PostId",
                table: "Notifications",
                column: "PostCommentReactNotification_PostId",
                principalTable: "Posts",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReactNotification_Post_PostId",
                table: "Notifications",
                column: "PostReactNotification_PostId",
                principalTable: "Posts",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostShareNotification_ShareablePost_PostId",
                table: "Notifications",
                column: "PostShareNotification_PostId",
                principalTable: "Posts",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostShareNotification_PostShare_PostShareId",
                table: "Notifications",
                column: "PostShareId",
                principalTable: "Shares",
                principalColumn: "ShareId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareNotification_Share_ShareId",
                table: "Notifications",
                column: "ShareId",
                principalTable: "Shares",
                principalColumn: "ShareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Shares_PostShareId",
                table: "Posts",
                column: "PostShareId",
                principalTable: "Shares",
                principalColumn: "ShareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_QuranHubUserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shares_AspNetUsers_QuranHubUserId",
                table: "Shares");

            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Posts_ShareablePostPostId",
                table: "Shares");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PrivacySettings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Reacts");

            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "VideosInfo");

            migrationBuilder.DropTable(
                name: "PlayListsInfo");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Shares");

            migrationBuilder.DropTable(
                name: "Verses");
        }
    }
}
