<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    exclude-result-prefixes="func"
    xmlns:func="urn:XsltFunctions">
  <xsl:template match="RegList">
    <root>
      <xsl:for-each select="Registration">
        <trans>
          <xsl:attribute name="playerid">
            <xsl:value-of select="PlayerId" />
          </xsl:attribute>
          <xsl:attribute name="username">
            <xsl:value-of select="UserName" />
          </xsl:attribute>
          <xsl:attribute name="currency">
            <xsl:value-of select="Currency" />
          </xsl:attribute>
          <xsl:attribute name="country">
            <xsl:value-of select="Country" />
          </xsl:attribute>
          <xsl:attribute name="btag">
            <xsl:value-of select="Btag" />
          </xsl:attribute>
          <xsl:attribute name="email">
            <xsl:value-of select="Email" />
          </xsl:attribute>
          <xsl:attribute name="firstname">
            <xsl:value-of select="FirstName" />
          </xsl:attribute>
          <xsl:attribute name="lastname">
            <xsl:value-of select="LastName" />
          </xsl:attribute>
          <xsl:attribute name="phone">
            <xsl:value-of select="Phone" />
          </xsl:attribute>
          <xsl:attribute name="address">
            <xsl:value-of select="Address" />
          </xsl:attribute>
          <xsl:attribute name="affid">
            <xsl:value-of select="func:GetAffId(Btag)" />
          </xsl:attribute>
          <xsl:attribute name="clickid">
            <xsl:value-of select="func:GetClickId(Btag)" />
          </xsl:attribute>
        </trans>
      </xsl:for-each>
    </root>
  </xsl:template>
</xsl:stylesheet>